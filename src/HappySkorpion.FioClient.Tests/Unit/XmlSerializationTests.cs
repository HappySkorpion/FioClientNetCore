using FluentAssertions;
using HappySkorpion.FioClient.Internal;
using HappySkorpion.FioClient.Internal.Dtos;
using HappySkorpion.FioClient.Tests.Unit.Helpers;
using System;
using System.Globalization;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Unit
{
    [Trait("Category", "Unit")]
    public class XmlSerializationTests
    {
        [Fact]
        public void DeserializeAccountStatementFromXml_Pass()
        {
            var statementXml = @"
                <AccountStatement>
                <Info>
                <accountId>2111111111</accountId>
                <bankId>2010</bankId>
                <currency>CZK</currency>
                <iban>CZ7920100000002111111111</iban>
                <bic>FIOBCZPPXXX</bic>
                <openingBalance>7356.22</openingBalance>
                <closingBalance>7321.22</closingBalance>
                <dateStart>2012-07-01+02:00</dateStart>
                <dateEnd>2012-07-31+02:00</dateEnd>
                <idFrom>1147608196</idFrom>
                <idTo>1147608197</idTo>
                <yearList>2012</yearList>
                <idList>4</idList>
                <idLastDownload>1147608196</idLastDownload>
                </Info>
                <TransactionList>
                <Transaction>
                <column_22 id=""22"" name=""ID pohybu"">1147608196</column_22>
                <column_0 id=""0"" name=""Datum"">2012-07-27+02:00</column_0>
                <column_1 id=""1"" name=""Objem"">-15.00</column_1>
                <column_14 id=""14"" name=""Měna"">CZK</column_14>
                <column_2 id=""2"" name=""Protiúčet"">2222233333</column_2>
                <column_3 id=""3"" name=""Kód banky"">2010</column_3>
                <column_12 id=""12"" name=""Název banky"">Fio banka, a.s.</column_12>
                <column_7 id=""7"" name=""Uživatelská identifikace""></column_7>
                <column_8 id=""8"" name=""Typ"">Platba převodem uvnitř banky</column_8>
                <column_9 id=""9"" name=""Provedl"">Novák, Jan</column_9>
                <column_25 id=""25"" name=""Komentář"">Můj test</column_25>
                <column_17 id=""17"" name=""ID pokynu"">2102392862</column_17>
                </Transaction>
                <Transaction>
                <column_22 id=""22"" name=""ID pohybu"">1147608197</column_22>
                <column_0 id=""0"" name=""Datum"">2012-07-27+02:00</column_0>
                <column_1 id=""1"" name=""Objem"">-20.00</column_1>
                <column_14 id=""14"" name=""Měna"">CZK</column_14>
                <column_2 id=""2"" name=""Protiúčet"">2222233333</column_2>
                <column_3 id=""3"" name=""Kód banky"">2010</column_3>
                <column_12 id=""12"" name=""Název banky"">Fio banka, a.s.</column_12>
                <column_7 id=""7"" name=""Uživatelská identifikace""></column_7>
                <column_8 id=""8"" name=""Typ"">Platba převodem uvnitř banky</column_8>
                <column_9 id=""9"" name=""Provedl"">Novák, Jan</column_9>
                <column_25 id=""25"" name=""Komentář""></column_25>
                <column_17 id=""17"" name=""ID pokynu"">2102392863</column_17>
                </Transaction>
                </TransactionList>
                </AccountStatement>";

            var expectedStatement = new AccountStatement
            {
                Info = new Info
                {
                    AccountId = "2111111111",
                    BankId = "2010",
                    Currency = "CZK",
                    Iban = "CZ7920100000002111111111",
                    Bic = "FIOBCZPPXXX",
                    OpeningBalance = 7356.22M,
                    ClosingBalance = 7321.22M,
                    DateStart = "2012-07-01+02:00",
                    DateEnd = "2012-07-31+02:00",
                    IdFrom = 1147608196,
                    IdTo = 1147608197,
                    YearList = 2012,
                    IdList = 4,
                    IdLastDownload = 1147608196,
                },
                Transactions = new Transaction[]
                    {
                        new Transaction
                        {
                            Id = 1147608196,
                            Date = "2012-07-27+02:00",
                            Amount = -15.00m,
                            Currency = "CZK",
                            CounterpartAccount = "2222233333",
                            CounterpartBankCode = "2010",
                            CounterpartBankName = "Fio banka, a.s.",
                            Identification = "",
                            Type = "Platba převodem uvnitř banky",
                            Accountant = "Novák, Jan",
                            Comment = "Můj test",
                            InstructionId = 2102392862,
                        },
                        new Transaction
                        {
                            Id = 1147608197,
                            Date = "2012-07-27+02:00",
                            Amount = -20.00m,
                            Currency = "CZK",
                            CounterpartAccount = "2222233333",
                            CounterpartBankCode = "2010",
                            CounterpartBankName = "Fio banka, a.s.",
                            Identification = "",
                            Type = "Platba převodem uvnitř banky",
                            Accountant = "Novák, Jan",
                            Comment = "",
                            InstructionId = 2102392863,
                        },
                    }
            };

            var deserializedStatement = XmlSerializerHelper.Deserialize<AccountStatement>(statementXml.ToStream());

            deserializedStatement.Should().BeEquivalentTo(expectedStatement);
        }

        [Fact]
        public void SerializeDomesticTransactionsImportIntoXml_Pass()
        {
            var domesticImport = new Import<DomesticTransaction>
            { 
                Orders = new DomesticTransaction[]
                {
                    new DomesticTransaction
                    {
                        AccountFrom = "AccountFrom",
                        AccountTo = "AccountTo",
                        Amount = 20.55m,
                        BankCode = "BankCode",
                        Comment = "Comment",
                        ConstantSymbol = "ConstantSymbol",
                        Currency = CurrencyCode.CZK,
                        Date = "2020-10-10",
                        MessageForRecipient = "MessageForReceipient",
                        PaymentReason = PaymentReason.Reason110,
                        PaymentType = PaymentType.Standard,
                        SpecificSymbol = "SpecificSymbol",
                        VariableSymbol = "VariableSymbol",                        
                    }
                }
            };

            var expectedImport =
@"<?xml version=""1.0"" encoding=""utf-16""?>
<Import xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xsi:noNamespaceSchemaLocation=""http://www.fio.cz/schema/importIB.xsd"">
  <Orders>
    <DomesticTransaction>
      <accountFrom>AccountFrom</accountFrom>
      <amount>20.55</amount>
      <currency>CZK</currency>
      <bankCode>BankCode</bankCode>
      <accountTo>AccountTo</accountTo>
      <ks>ConstantSymbol</ks>
      <vs>VariableSymbol</vs>
      <ss>SpecificSymbol</ss>
      <date>2020-10-10</date>
      <messageForRecipient>MessageForReceipient</messageForRecipient>
      <comment>Comment</comment>
      <paymentReason>110</paymentReason>
      <paymentType>431008</paymentType>
    </DomesticTransaction>
  </Orders>
</Import>";

            var serializedImport = XmlSerializerHelper.Serialize(domesticImport);

            serializedImport.Should().BeEquivalentTo(expectedImport);
        }

        [Fact]
        public void SerializeT2TransactionsImportIntoXml_Pass()
        {
            var domesticImport = new Import<T2Transaction>
            {
                Orders = new T2Transaction[]
                {
                    new T2Transaction
                    {
                        AccountFrom = "AccountFrom",
                        AccountTo = "AccountTo",
                        Amount = 100.00m,
                        Comment = "Comment",
                        ConstantSymbol = "ConstantSymbol",
                        Currency = CurrencyCode.EUR,
                        Bic = "Bic",
                        Date = "2020-10-10",
                        PaymentReason = PaymentReason.Reason110,
                        PaymentType = PaymentType.Standard,
                        SpecificSymbol = "SpecificSymbol",
                        VariableSymbol = "VariableSymbol",
                        BenefName = "BenefName",
                        BenefStreet = "BenefStreet",
                        BenefCity = "BenefCity",
                        BenefCountry = "BenefCountry",
                        RemittanceInfo1 = "RemittanceInfo1",
                        RemittanceInfo2 = "RemittanceInfo2",
                        RemittanceInfo3 = "RemittanceInfo3",
                    }
                }
            };

            var expectedImport =
@"<?xml version=""1.0"" encoding=""utf-16""?>
<Import xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xsi:noNamespaceSchemaLocation=""http://www.fio.cz/schema/importIB.xsd"">
  <Orders>
    <T2Transaction>
      <accountFrom>AccountFrom</accountFrom>
      <amount>100.00</amount>
      <currency>EUR</currency>
      <accountTo>AccountTo</accountTo>
      <ks>ConstantSymbol</ks>
      <vs>VariableSymbol</vs>
      <ss>SpecificSymbol</ss>
      <bic>Bic</bic>
      <date>2020-10-10</date>
      <comment>Comment</comment>
      <benefName>BenefName</benefName>
      <benefStreet>BenefStreet</benefStreet>
      <benefCity>BenefCity</benefCity>
      <benefCountry>BenefCountry</benefCountry>
      <remittanceInfo1>RemittanceInfo1</remittanceInfo1>
      <remittanceInfo2>RemittanceInfo2</remittanceInfo2>
      <remittanceInfo3>RemittanceInfo3</remittanceInfo3>
      <paymentReason>110</paymentReason>
      <paymentType>431008</paymentType>
    </T2Transaction>
  </Orders>
</Import>";

            var serializedImport = XmlSerializerHelper.Serialize(domesticImport);

            serializedImport.Should().BeEquivalentTo(expectedImport);
        }

        [Fact]
        public void SerializeForeignTransactionsImportIntoXml_Pass()
        {
            var domesticImport = new Import<ForeignTransaction>
            {
                Orders = new ForeignTransaction[]
                {
                    new ForeignTransaction
                    {
                        AccountFrom = "AccountFrom",
                        AccountTo = "AccountTo",
                        Amount = 100.00m,
                        Comment = "Comment",
                        Currency = CurrencyCode.USD,
                        Bic = "Bic",
                        Date = "2020-10-10",
                        BenefName = "BenefName",
                        BenefStreet = "BenefStreet",
                        BenefCity = "BenefCity",
                        BenefCountry = "BenefCountry",
                        RemittanceInfo1 = "RemittanceInfo1",
                        RemittanceInfo2 = "RemittanceInfo2",
                        RemittanceInfo3 = "RemittanceInfo3",
                        RemittanceInfo4 = "RemittanceInfo4",
                        DetailsOfCharges = ChargeType.OUR,
                        PaymentReason = PaymentReason.Reason110
                    }
                }
            };

            var expectedImport =
@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Import xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xsi:noNamespaceSchemaLocation=""http://www.fio.cz/schema/importIB.xsd"">
  <Orders>
    <ForeignTransaction>
      <accountFrom>AccountFrom</accountFrom>
      <amount>100.00</amount>
      <currency>USD</currency>
      <accountTo>AccountTo</accountTo>
      <bic>Bic</bic>
      <date>2020-10-10</date>
      <comment>Comment</comment>
      <benefName>BenefName</benefName>
      <benefStreet>BenefStreet</benefStreet>
      <benefCity>BenefCity</benefCity>
      <benefCountry>BenefCountry</benefCountry>
      <remittanceInfo1>RemittanceInfo1</remittanceInfo1>
      <remittanceInfo2>RemittanceInfo2</remittanceInfo2>
      <remittanceInfo3>RemittanceInfo3</remittanceInfo3>
      <remittanceInfo4>RemittanceInfo4</remittanceInfo4>
      <detailsOfCharges>470501</detailsOfCharges>
      <paymentReason>110</paymentReason>
    </ForeignTransaction>
  </Orders>
</Import>";

            var serializedImport = XmlSerializerHelper.Serialize(domesticImport);

            serializedImport.Should().BeEquivalentTo(expectedImport);
        }
    }
}
