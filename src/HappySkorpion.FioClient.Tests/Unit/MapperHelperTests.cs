using FluentAssertions;
using HappySkorpion.FioClient.Internal;
using HappySkorpion.FioClient.Internal.Dtos;
using System.Collections.Generic;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Unit
{

    [Trait("Category", "Unit")]
    public class MapperHelperTests
    {
        [Fact]
        public void MapToAccountTransactionsResult_Pass()
        {
            var statement = new AccountStatement
            {
                Info = new Info
                {
                    AccountId = "AccountId1",
                    BankId = "BankId1",
                    Bic = "Bic1",
                    ClosingBalance = 10,
                    Currency = "Currency1",
                    DateEnd = "DateEnd1",
                    DateStart = "DateStart1",
                    Iban = "Iban1",
                    IdFrom = 11,
                    IdLastDownload = 12,
                    IdList = 13,
                    IdTo = 14,
                    OpeningBalance = 15,
                    YearList = 16,
                },
                Transactions = new Internal.Dtos.Transaction[]
                    {
                        new Internal.Dtos.Transaction
                        {
                            Accountant = "Accountant11",
                            Amount = 110,
                            Comment = "Comment11",
                            ConstantSymbol = "ConstantSymbol11",
                            CounterpartAccount = "CounterpartAccount11",
                            CounterpartAccountName = "CounterpartAccountName11",
                            CounterpartBankCode = "CounterpartBankCode11",
                            CounterpartBankName = "CounterpartBankName11",
                            Currency = "Currency11",
                            Date = "111",
                            Id = 111,
                            Identification = "Identification11",
                            InstructionId = 112,
                            MessageForReceipient = "MessageForReceipient11",
                            SpecificSymbol = "SpecificSymbol11",
                            Type = "Type11",
                            VariableSymbol = "VariableSymbol11",
                        },
                        new Internal.Dtos.Transaction
                        {
                            Accountant = "Accountant12",
                            Amount = 120,
                            Comment = "Comment12",
                            ConstantSymbol = "ConstantSymbol12",
                            CounterpartAccount = "CounterpartAccount12",
                            CounterpartAccountName = "CounterpartAccountName12",
                            CounterpartBankCode = "CounterpartBankCode12",
                            CounterpartBankName = "CounterpartBankName12",
                            Currency = "Currency12",
                            Date = "121",
                            Id = 121,
                            Identification = "Identification12",
                            InstructionId = 122,
                            MessageForReceipient = "MessageForReceipient12",
                            SpecificSymbol = "SpecificSymbol12",
                            Type = "Type12",
                            VariableSymbol = "VariableSymbol12",
                        }
                    },
            };

            var expectedTransactionsResult = new AccountTransactionsResult
            {
                Account = new Account
                {
                    AccountId = "AccountId1",
                    BankId = "BankId1",
                    Bic = "Bic1",
                    ClosingBalance = 10,
                    Currency = "Currency1",
                    Iban = "Iban1",
                    OpeningBalance = 15,
                },
                Metadata = new AccountTransactionsMetadata
                {
                    DateEnd = "DateEnd1",
                    DateStart = "DateStart1",
                    IdFrom = 11,
                    IdLastDownload = 12,
                    IdList = 13,
                    IdTo = 14,
                    YearList = 16,
                },
                Transactions = new AccountTransaction[] 
                { 
                    new AccountTransaction
                    {
                        Accountant = "Accountant11",
                        Amount = 110,
                        Comment = "Comment11",
                        ConstantSymbol = "ConstantSymbol11",
                        CounterpartAccount = "CounterpartAccount11",
                        CounterpartAccountName = "CounterpartAccountName11",
                        CounterpartBankCode = "CounterpartBankCode11",
                        CounterpartBankName = "CounterpartBankName11",
                        Currency = "Currency11",
                        Date = "111",
                        Id = 111,
                        Identification = "Identification11",
                        InstructionId = 112,
                        MessageForReceipient = "MessageForReceipient11",
                        SpecificSymbol = "SpecificSymbol11",
                        Type = "Type11",
                        VariableSymbol = "VariableSymbol11",
                    },
                    new AccountTransaction
                    {
                        Accountant = "Accountant12",
                        Amount = 120,
                        Comment = "Comment12",
                        ConstantSymbol = "ConstantSymbol12",
                        CounterpartAccount = "CounterpartAccount12",
                        CounterpartAccountName = "CounterpartAccountName12",
                        CounterpartBankCode = "CounterpartBankCode12",
                        CounterpartBankName = "CounterpartBankName12",
                        Currency = "Currency12",
                        Date = "121",
                        Id = 121,
                        Identification = "Identification12",
                        InstructionId = 122,
                        MessageForReceipient = "MessageForReceipient12",
                        SpecificSymbol = "SpecificSymbol12",
                        Type = "Type12",
                        VariableSymbol = "VariableSymbol12",
                    }
                }
            };

            var mappedTransactionResult = MapperHelper.MapToAccountTransactionsResult(statement);

            mappedTransactionResult.Should().BeEquivalentTo(expectedTransactionsResult);
        }

        [Fact]
        public void MapToOrdersResult_Pass()
        {
            var statement = new ResponseImport
            {
                Result = new Result
                {
                    ErrorCode = 1,
                    IdInstruction = 2,
                    Message = "message",
                    Status = "warning",
                    Sums = new[] 
                    {
                        new Sum
                        {
                            Id = "CZK",
                            SumCredit = 50M,
                            SumDebet = 0M,
                        }
                    }
                },
                Orders = new [] 
                {
                    new OrderDetail
                    {
                        Id = 1,
                        Messages = new OrderDetailMessage[] 
                        {
                            new OrderDetailMessage 
                            { 
                                ErrorCode = 1, 
                                Status = "warning",
                                Text = "Text test",
                            }
                        }
                    }
                },
            };

            var expectedOrdersResult = new OrdersResult
            {
               ErrorCode = 1,
               IdInstruction = 2,
               Status = "warning",
               OrderResults = new List<OrderResult>(new [] 
               {
                   new OrderResult
                   {
                       Id = 1,
                       Messages = new List<OrderResultMessage> (new [] 
                       {
                           new OrderResultMessage
                           {
                               ErrorCode = 1,
                               Status = "warning",
                               Text = "Text test",
                           }
                       }),
                   }
               }),
            };

            var mappedTransactionResult = MapperHelper.MapToOrdersResult(statement);

            mappedTransactionResult.Should().BeEquivalentTo(expectedOrdersResult);
        }

        [Fact]
        public void MapDomesticTransactionOrdersToDomesticTransactionImport_Pass()
        {
            var orders = new []
            { 
                new DomesticTransactionOrder
                {
                    SourceAccountNumber = "AccountFrom",
                    Amount = 100,
                    Currency = CurrencyCode.CZK,
                    DestinationAccountBank = "BankCode",
                    DestinationAccountNumber = "AccountTo",
                    ConstantSymbol = "ConstantSymbol",
                    VariableSymbol = "VariableSymbol",
                    SpecificSymbol = "SpecificSymbol",
                    Date = "Date",
                    MessageForRecipient = "MessageForReceipient",
                    Comment = "Comment",
                    PaymentReason = PaymentReason.Reason110,
                    PaymentType = DomesticPaymentType.Standard,
                }
            };

            var expectedImport = new Import<DomesticTransaction>
            {
                Orders = new []
                {
                    new DomesticTransaction
                    {
                        AccountFrom = "AccountFrom",
                        Amount = 100,
                        Currency = CurrencyCode.CZK,
                        BankCode = "BankCode",
                        AccountTo = "AccountTo",
                        ConstantSymbol = "ConstantSymbol",
                        VariableSymbol = "VariableSymbol",
                        SpecificSymbol = "SpecificSymbol",
                        Date = "Date",
                        MessageForRecipient = "MessageForReceipient",
                        Comment = "Comment",
                        PaymentReason = PaymentReason.Reason110,
                        PaymentType = DomesticPaymentType.Standard,
                    },
                }
            };

            var mappedImport = MapperHelper.MapToDomesticTransactionsImport(orders);

            mappedImport.Should().BeEquivalentTo(expectedImport);
        }

        [Fact]
        public void MapEuroTransactionOrdersToEuroImport_Pass()
        {
            var orders = new[]
            {
                new EuroTransactionOrder
                {
                    SourceAccountNumber = "AccountFrom",
                    Amount = 100,
                    Currency = CurrencyCode.USD,
                    DestinationAccountNumber = "AccountTo",
                    ConstantSymbol = "ConstantSymbol",
                    VariableSymbol = "VariableSymbol",
                    SpecificSymbol = "SpecificSymbol",
                    Bic = "Bic",
                    Date = "Date",
                    Comment = "Comment",
                    BenefName = "BenefName",
                    BenefStreet = "BenefStreet",
                    BenefCity = "BenefCity",
                    BenefCountry = "BenefCountry",
                    RemittanceInfo1 = "RemittanceInfo1",
                    RemittanceInfo2 = "RemittanceInfo2",
                    RemittanceInfo3 = "RemittanceInfo3",
                    PaymentReason = PaymentReason.Reason110,
                    PaymentType = EuroPaymentType.Standard,
                }
            };

            var expectedImport = new Import<T2Transaction>
            {
                Orders = new[]
                {
                    new T2Transaction
                    {
                         AccountFrom = "AccountFrom",
                        Amount = 100,
                        Currency = CurrencyCode.USD,
                        AccountTo = "AccountTo",
                        ConstantSymbol = "ConstantSymbol",
                        VariableSymbol = "VariableSymbol",
                        SpecificSymbol = "SpecificSymbol",
                        Bic = "Bic",
                        Date = "Date",
                        Comment = "Comment",
                        BenefName = "BenefName",
                        BenefStreet = "BenefStreet",
                        BenefCity = "BenefCity",
                        BenefCountry = "BenefCountry",
                        RemittanceInfo1 = "RemittanceInfo1",
                        RemittanceInfo2 = "RemittanceInfo2",
                        RemittanceInfo3 = "RemittanceInfo3",
                        PaymentReason = PaymentReason.Reason110,
                        PaymentType = EuroPaymentType.Standard,
                    },
                }
            };

            var mappedImport = MapperHelper.MapToT2TransactionsImport(orders);

            mappedImport.Should().BeEquivalentTo(expectedImport);
        }

        [Fact]
        public void MapForeignTransactionOrdersToForeignImport_Pass()
        {
            var orders = new[]
            {
                new ForeignTransactionOrder
                {
                    SourceAccountNumber = "AccountFrom",
                    Amount = 100m,
                    Currency = CurrencyCode.USD,
                    DestinationAccountNumber = "AccountTo",
                    Bic = "Bic",
                    Date = "Date",
                    Comment = "Comment",
                    BenefName = "BenefName",
                    BenefStreet = "BenefStreet",
                    BenefCity = "BenefCity",
                    BenefCountry = "BenefCountry",
                    RemittanceInfo1 = "RemittanceInfo1",
                    RemittanceInfo2 = "RemittanceInfo2",
                    RemittanceInfo3 = "RemittanceInfo3",
                    RemittanceInfo4 = "RemittanceInfo4",
                    DetailsOfCharges = ChargeType.SHA,
                    PaymentReason = PaymentReason.Reason110,
                }
            };

            var expectedImport = new Import<ForeignTransaction>
            {
                Orders = new[]
                {
                    new ForeignTransaction
                    {
                        AccountFrom = "AccountFrom",
                        Amount = 100m,
                        Currency = CurrencyCode.USD,
                        AccountTo = "AccountTo",
                        Bic = "Bic",
                        Date = "Date",
                        Comment = "Comment",
                        BenefName = "BenefName",
                        BenefStreet = "BenefStreet",
                        BenefCity = "BenefCity",
                        BenefCountry = "BenefCountry",
                        RemittanceInfo1 = "RemittanceInfo1",
                        RemittanceInfo2 = "RemittanceInfo2",
                        RemittanceInfo3 = "RemittanceInfo3",
                        RemittanceInfo4 = "RemittanceInfo4",
                        DetailsOfCharges = ChargeType.SHA,
                        PaymentReason = PaymentReason.Reason110,
                    },
                }
            };

            var mappedImport = MapperHelper.MapToForeignTransactionsImport(orders);

            mappedImport.Should().BeEquivalentTo(expectedImport);
        }
    }
}
