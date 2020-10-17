using FluentValidation;
using HappySkorpion.FioClient.Internal.Validations.Helpers;
using System.ComponentModel;

namespace HappySkorpion.FioClient.Internal.Validations
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class ForeignTransactionOrderValidation
        : AbstractValidator<ForeignTransactionOrder>
    {
        public ForeignTransactionOrderValidation()
        {
            RuleFor(x => x.SourceAccountNumber)
                .NotEmpty()
                .InternalBankAccountNumber();

            RuleFor(x => x.Amount)
                .NotEmpty()
                .Amount();

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Currency();

            RuleFor(x => x.DestinationAccountNumber)
                .NotEmpty()
                .Iban();

            RuleFor(x => x.Bic)
                .NotEmpty()
                .Bic();

            RuleFor(x => x.Date)
                .NotEmpty()
                .Date();

            RuleFor(x => x.Comment)
                .EuroComment();

            RuleFor(x => x.BenefName)
                .NotEmpty()
                .BenefName();

            RuleFor(x => x.BenefStreet)
                .BenefStreet();

            RuleFor(x => x.BenefCity)
                .BenefCity();

            RuleFor(x => x.BenefCountry)
                .BenefCountry();

            RuleFor(x => x.RemittanceInfo1)
                .RemittanceInfo();

            RuleFor(x => x.RemittanceInfo2)
                .RemittanceInfo();

            RuleFor(x => x.RemittanceInfo3)
                .RemittanceInfo();

            RuleFor(x => x.RemittanceInfo4)
                .RemittanceInfo();

            RuleFor(x => x.DetailsOfCharges)
                .NotEmpty()
                .ChargeType();

            RuleFor(x => x.PaymentReason)
                .NotEmpty()
                .PaymentReason();
        }
    }
}
