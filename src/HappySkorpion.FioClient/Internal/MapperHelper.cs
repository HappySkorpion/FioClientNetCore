using HappySkorpion.FioClient.Internal.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace HappySkorpion.FioClient.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public static class MapperHelper
    {
        public static AccountTransactionsResult MapToAccountTransactionsResult(
            AccountStatement statement)
        {
            _ = statement ?? throw new ArgumentNullException(nameof(statement));

            var info = statement.Info;
            var transactions = statement.Transactions;

            return new AccountTransactionsResult
            {
                Account = new Account
                {
                    AccountId = info.AccountId,
                    BankId = info.BankId,
                    Currency = info.Currency,
                    Iban = info.Iban,
                    Bic = info.Bic,
                    OpeningBalance = info.OpeningBalance,
                    ClosingBalance = info.ClosingBalance,
                },
                Metadata = new AccountTransactionsMetadata
                {
                    DateStart = info.DateStart,
                    DateEnd = info.DateEnd,
                    YearList = info.YearList,
                    IdList = info.IdList,
                    IdFrom = info.IdFrom,
                    IdTo = info.IdTo,
                    IdLastDownload = info.IdLastDownload,
                },
                Transactions = transactions.Select(x => new AccountTransaction
                {
                    Id = x.Id,
                    CounterpartAccount = x.CounterpartAccount,
                    CounterpartAccountName = x.CounterpartAccountName,
                    CounterpartBankCode = x.CounterpartBankCode,
                    CounterpartBankName = x.CounterpartBankName,
                    Date = x.Date,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    VariableSymbol = x.VariableSymbol,
                    ConstantSymbol = x.ConstantSymbol,
                    SpecificSymbol = x.SpecificSymbol,
                    Comment = x.Comment,
                    MessageForReceipient = x.MessageForReceipient,
                    Type = x.Type,
                    Identification = x.Identification,
                    InstructionId = x.InstructionId,
                    Accountant = x.Accountant,
                })
                    .ToList(),
            };
        }

        public static Import<DomesticTransaction> MapToDomesticTransactionsImport(
            IEnumerable<DomesticTransactionOrder> paymentOrders)
        {
            return new Import<DomesticTransaction>
            {
                Orders = paymentOrders.Select(p => new DomesticTransaction
                {
                    AccountFrom = p.SourceAccountNumber,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    BankCode = p.DestinationAccountBank,
                    AccountTo = p.DestinationAccountNumber,
                    ConstantSymbol = p.ConstantSymbol,
                    VariableSymbol = p.VariableSymbol,
                    SpecificSymbol = p.SpecificSymbol,
                    Date = p.Date,
                    MessageForRecipient = p.MessageForRecipient,
                    Comment = p.Comment,
                    PaymentReason = p.PaymentReason,
                    PaymentType = p.PaymentType,
                })
                .ToArray(),
            };
        }

        public static Import<T2Transaction> MapToT2TransactionsImport(
            IEnumerable<EuroTransactionOrder> paymentOrders)
        {
            return new Import<T2Transaction>
            {
                Orders = paymentOrders.Select(p => new T2Transaction
                {
                    AccountFrom = p.SourceAccountNumber,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    AccountTo = p.DestinationAccountNumber,
                    ConstantSymbol = p.ConstantSymbol,
                    VariableSymbol = p.VariableSymbol,
                    SpecificSymbol = p.SpecificSymbol,
                    Bic = p.Bic,
                    Date = p.Date,
                    Comment = p.Comment,
                    BenefName = p.BenefName,
                    BenefStreet = p.BenefStreet,
                    BenefCity = p.BenefCity,
                    BenefCountry = p.BenefCountry,
                    RemittanceInfo1 = p.RemittanceInfo1,
                    RemittanceInfo2 = p.RemittanceInfo2,
                    RemittanceInfo3 = p.RemittanceInfo3,
                    PaymentReason = p.PaymentReason,
                    PaymentType = p.PaymentType,
                })
                .ToArray(),
            };
        }

        public static Import<ForeignTransaction> MapToForeignTransactionsImport(
            IEnumerable<ForeignTransactionOrder> paymentOrders)
        {
            return new Import<ForeignTransaction>
            {
                Orders = paymentOrders.Select(p => new ForeignTransaction
                {
                    AccountFrom = p.SourceAccountNumber,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    AccountTo = p.DestinationAccountNumber,
                    Bic = p.Bic,
                    Date = p.Date,
                    Comment = p.Comment,
                    BenefName = p.BenefName,
                    BenefStreet = p.BenefStreet,
                    BenefCity = p.BenefCity,
                    BenefCountry = p.BenefCountry,
                    RemittanceInfo1 = p.RemittanceInfo1,
                    RemittanceInfo2 = p.RemittanceInfo2,
                    RemittanceInfo3 = p.RemittanceInfo3,
                    RemittanceInfo4 = p.RemittanceInfo4,
                    DetailsOfCharges = p.DetailsOfCharges,
                    PaymentReason = p.PaymentReason,
                })
                .ToArray(),
            };
        }
    }
}
