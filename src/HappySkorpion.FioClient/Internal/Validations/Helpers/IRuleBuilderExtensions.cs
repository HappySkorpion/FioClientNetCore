using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappySkorpion.FioClient.Internal.Validations.Helpers
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public static class IRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> InternalBankAccountNumber<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{1,16}$")
                .WithErrorCode("InternalAccountNumberValidator")
                .WithMessage(@"Invalid internal account number. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> DomesticBankAccountNumber<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{1,6}-\d{1,10}$")
                .WithErrorCode("DomesticAccountNumberValidator")
                .WithMessage(@"Invalid domestic account number. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> Iban<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Iban(new IbanValidator())
                .WithErrorCode("IbanValidator");
        }

        public static IRuleBuilderOptions<T, decimal> Amount<T>(
            this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0m);
        }

        public static IRuleBuilderOptions<T, CurrencyCode> Currency<T>(
            this IRuleBuilder<T, CurrencyCode> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum()
                .WithErrorCode("CurrencyValidator")
                .WithMessage($"Invalid currency. The valid one has to be one of velues from enum type CurrencyCode or null.");
        }

        public static IRuleBuilderOptions<T, CurrencyCode> EuroCurrency<T>(
            this IRuleBuilder<T, CurrencyCode> ruleBuilder)
        {
            return ruleBuilder
                .Must(currency => currency == CurrencyCode.EUR)
                .WithErrorCode("CurrencyValidator")
                .WithMessage($"Invalid currency. The valid one has to be EUR.");
        }

        public static IRuleBuilderOptions<T, string> BankCode<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{4}$")
                .WithErrorCode("BankCodeValidator")
                .WithMessage(@"Invalid conbank code. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> ConstantSymbol<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{4}$")
                .WithErrorCode("ConstantSymbolValidator")
                .WithMessage(@"Invalid constant symbol. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> VariableSymbol<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{1,10}$")
                .WithErrorCode("VariableSymbolValidator")
                .WithMessage(@"Invalid constant symbol. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> SpecificSymbol<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{1,10}$")
                .WithErrorCode("SpecificSymbolValidator")
                .WithMessage(@"Invalid specific symbol. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        public static IRuleBuilderOptions<T, string> Bic<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-z]{6}[0-9a-z]{2}([0-9a-z]{3})?$", RegexOptions.IgnoreCase)
                .WithErrorCode("BicValidator")
                .WithMessage(@"Invalid Bic. The valid one has to satisfy the pattern ""{RegularExpression}"".");
        }

        private const string DateFormat = "yyyy-MM-dd";
        
        public static IRuleBuilderOptions<T, string> Date<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(date => DateTime.TryParseExact(date, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var _))
                .WithErrorCode("DateValidator")
                .WithMessage($"Invalid date. The valid one has to satisfy the pattern \"{DateFormat}\".");
        }

        public static IRuleBuilderOptions<T, string> EuroComment<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(140);
        }

        public static IRuleBuilderOptions<T, string> DomesticComment<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(255);
        }

        public static IRuleBuilderOptions<T, string> MessageForReceipient<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(140);
        }

        public static IRuleBuilderOptions<T, string> BenefName<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(35);
        }

        public static IRuleBuilderOptions<T, string> BenefStreet<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(35);
        }

        public static IRuleBuilderOptions<T, string> BenefCity<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(35);
        }

        public static IRuleBuilderOptions<T, string> BenefCountry<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(3);
        }

        public static IRuleBuilderOptions<T, string> RemittanceInfo<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(35);
        }

        private readonly static PaymentType[] DomesticPaymentTypes = new[] 
        { 
            PaymentType.Standard, 
            PaymentType.Precedential, 
            PaymentType.Collection,
        };

        public static IRuleBuilderOptions<T, PaymentType?> DomesticPaymentType<T>(
            this IRuleBuilder<T, PaymentType?> ruleBuilder)
        {
            return ruleBuilder
                .Must(x => !x.HasValue || DomesticPaymentTypes.Contains(x.Value))
                .WithErrorCode("PaymentTypeValidator")
                .WithMessage($"Invalid payment type. The valid one has to be one of {string.Join(", ", DomesticPaymentTypes)}.");
        }


        private readonly static PaymentType[] EuroPaymentTypes = new[]
        {
            PaymentType.Standard,
            PaymentType.Precedential,
        };

        public static IRuleBuilderOptions<T, PaymentType?> EuroPaymentType<T>(
            this IRuleBuilder<T, PaymentType?> ruleBuilder)
        {
            return ruleBuilder
                .Must(x => !x.HasValue || EuroPaymentTypes.Contains(x.Value))
                .WithErrorCode("PaymentTypeValidator")
                .WithMessage($"Invalid payment type. The valid one has to be one of {string.Join(", ", EuroPaymentTypes)}.");
        }

        public static IRuleBuilderOptions<T, PaymentReason?> PaymentReason<T>(
            this IRuleBuilder<T, PaymentReason?> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum()
                .WithErrorCode("PaymentReasonValidator")
                .WithMessage($"Invalid payment reason. The valid one has to be one of velues from enum type PaymentReason or null.");
        }

        public static IRuleBuilderOptions<T, PaymentReason> PaymentReason<T>(
            this IRuleBuilder<T, PaymentReason> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum()
                .WithErrorCode("PaymentReasonValidator")
                .WithMessage($"Invalid payment reason. The valid one has to be one of velues from enum type PaymentReason.");
        }

        public static IRuleBuilderOptions<T, ChargeType> ChargeType<T>(
            this IRuleBuilder<T, ChargeType> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum()
                .WithErrorCode("ChargeTypeValidator")
                .WithMessage($"Invalid charge type. The valid one has to be one of velues from enum type ChargeType.");
        }
    }
}
