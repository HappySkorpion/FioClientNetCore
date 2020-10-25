using FluentValidation;
using HappySkorpion.FioClient.Internal.Validations.Helpers;
using System.ComponentModel;

namespace HappySkorpion.FioClient.Internal.Validations
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class DomesticTransactionOrderValidation
        : AbstractValidator<DomesticTransactionOrder>
    {
        public DomesticTransactionOrderValidation()
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
                .DomesticBankAccountNumber();

            RuleFor(x => x.DestinationAccountBank)
                .NotEmpty()
                .BankCode();

            RuleFor(x => x.ConstantSymbol)
                .ConstantSymbol();

            RuleFor(x => x.VariableSymbol)
                .VariableSymbol();

            RuleFor(x => x.SpecificSymbol)
                .SpecificSymbol();

            RuleFor(x => x.Date)
                .NotEmpty()
                .Date();

            RuleFor(x => x.Comment)
                .DomesticComment();

            RuleFor(x => x.MessageForRecipient)
                .MessageForReceipient();

            RuleFor(x => x.PaymentReason)
                .PaymentReason();

            RuleFor(x => x.PaymentType)
                .DomesticPaymentType();
        }
    }
}
