using FluentAssertions;
using HappySkorpion.FioClient.Internal.Validations;
using System.Threading.Tasks;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Unit.Validators
{
    [Trait("Category", "Unit")]
    public class DomesticTransactionOrderValidationTests
    {
        private DomesticTransactionOrder GetValidOrder()
        {
            return new DomesticTransactionOrder
            {
                SourceAccountNumber = "123456798",
                Amount = 100,
                Currency = CurrencyCode.CZK,
                DestinationAccountBank = "0000",
                DestinationAccountNumber = "000000-123456798",
                ConstantSymbol = "0123",
                VariableSymbol = "643153135",
                SpecificSymbol = "8746135",
                Date = "2020-05-10",
                MessageForRecipient = "Message",
                Comment = "Comment",
                PaymentReason = PaymentReason.Reason110,
                PaymentType = PaymentType.Standard,
            };
        }

        [Fact]
        public async Task ValidOrder_Pass()
        {
            var order = GetValidOrder();

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { "1" })]
        [InlineData(new object[] { "12" })]
        [InlineData(new object[] { "123" })]
        [InlineData(new object[] { "1234" })]
        [InlineData(new object[] { "12345" })]
        [InlineData(new object[] { "123456" })]
        [InlineData(new object[] { "1234567" })]
        [InlineData(new object[] { "12345678" })]
        [InlineData(new object[] { "123456789" })]
        [InlineData(new object[] { "1234567890" })]
        [InlineData(new object[] { "12345678901" })]
        [InlineData(new object[] { "123456789012" })]
        [InlineData(new object[] { "1234567890123" })]
        [InlineData(new object[] { "12345678901234" })]
        [InlineData(new object[] { "123456789012345" })]
        [InlineData(new object[] { "1234567890123456" })]
        public async Task SourceAccountNumber_ValidInternalAccount_Pass(string accountNumber)
        {
            var order = GetValidOrder();
            order.SourceAccountNumber = accountNumber;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task SourceAccountNumber_Empty_Fail()
        {
            var order = GetValidOrder();
            order.SourceAccountNumber = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.SourceAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "wrong" })]
        [InlineData(new object[] { "12345678901234567" })]
        [InlineData(new object[] { "1234567890!A" })]
        public async Task SourceAccountNumber_InvalidInternalAccount_Fail(string accountNumber)
        {
            var order = GetValidOrder();
            order.SourceAccountNumber = accountNumber;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.SourceAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("InternalAccountNumberValidator");
        }

        [Fact]
        public async Task Amount_PositiveNumber_Pass()
        {
            var order = GetValidOrder();
            order.Amount = 15000;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Amount_ZeroNumber_Fail()
        {
            var order = GetValidOrder();
            order.Amount = 0;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Amount));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
            result.Errors[1].PropertyName.Should().Be(nameof(order.Amount));
            result.Errors[1].ErrorCode.Should().Be("GreaterThanValidator");
        }

        [Fact]
        public async Task Amount_NegativeNumber_Fail()
        {
            var order = GetValidOrder();
            order.Amount = -100;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Amount));
            result.Errors[0].ErrorCode.Should().Be("GreaterThanValidator");
        }

        [Theory]
        [InlineData(new object[] { CurrencyCode.CZK })]
        [InlineData(new object[] { CurrencyCode.EUR })]
        [InlineData(new object[] { CurrencyCode.USD })]
        [InlineData(new object[] { CurrencyCode.GBP })]
        [InlineData(new object[] { CurrencyCode.AUD })]
        public async Task Currency_ValidCurrency_Pass(CurrencyCode currency)
        {
            var order = GetValidOrder();
            order.Currency = currency;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Currency_Empty_Fail()
        {
            var order = GetValidOrder();
            order.Currency = default;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Currency));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Fact]
        public async Task Currency_InvalidCurrency_Fail()
        {
            var order = GetValidOrder();
            order.Currency = (CurrencyCode)100000;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Currency));
            result.Errors[0].ErrorCode.Should().Be("CurrencyValidator");
        }

        [Theory]
        [InlineData(new object[] { "1-1" })]
        [InlineData(new object[] { "12-1" })]
        [InlineData(new object[] { "123-1" })]
        [InlineData(new object[] { "1234-1" })]
        [InlineData(new object[] { "12345-1" })]
        [InlineData(new object[] { "123456-1" })]
        [InlineData(new object[] { "1-12" })]
        [InlineData(new object[] { "1-123" })]
        [InlineData(new object[] { "1-1234" })]
        [InlineData(new object[] { "1-12345" })]
        [InlineData(new object[] { "1-123456" })]
        [InlineData(new object[] { "1-1234567" })]
        [InlineData(new object[] { "1-12345678" })]
        [InlineData(new object[] { "1-123456789" })]
        [InlineData(new object[] { "1-1234567890" })]

        public async Task DestinationAccountNumber_ValidAccountNumber_Pass(string accountTo)
        {
            var order = GetValidOrder();
            order.DestinationAccountNumber = accountTo;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task DestinationAccountNumber_Empty_Fail(string accountTo)
        {
            var order = GetValidOrder();
            order.DestinationAccountNumber = accountTo;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "wrong" })]
        [InlineData(new object[] { "1234561234567890" })]
        [InlineData(new object[] { "-1234567890" })]
        [InlineData(new object[] { "1234567-1234567890" })]
        [InlineData(new object[] { "123456-" })]
        [InlineData(new object[] { "123456-12345678901" })]
        [InlineData(new object[] { "12345678!A" })]
        public async Task DestinationAccountNumber_InvalidAccountNumber_Fail(string accountTo)
        {
            var order = GetValidOrder();
            order.DestinationAccountNumber = accountTo;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("DomesticAccountNumberValidator");
        }


        [Theory]
        [InlineData(new object[] { "0001" })]
        [InlineData(new object[] { "1234" })]
        public async Task DestinationAccountBank_ValidDestinationAccountBank_Pass(string bankCode)
        {
            var order = GetValidOrder();
            order.DestinationAccountBank = bankCode;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task DestinationAccountBank_Empty_Fail()
        {
            var order = GetValidOrder();
            order.DestinationAccountBank = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountBank));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "1" })]
        [InlineData(new object[] { "12" })]
        [InlineData(new object[] { "123" })]
        [InlineData(new object[] { "12345" })]
        [InlineData(new object[] { "123456" })]
        [InlineData(new object[] { "abcd" })]
        public async Task DestinationAccountBank_InvalidDestinationAccountBank_Fail(string bankCode)
        {
            var order = GetValidOrder();
            order.DestinationAccountBank = bankCode;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountBank));
            result.Errors[0].ErrorCode.Should().Be("BankCodeValidator");
        }

        [Theory]
        [InlineData(new object[] { "0001" })]
        [InlineData(new object[] { "1234" })]
        public async Task ConstantSymbol_ValidConstantSymbol_Pass(string constantSymbol)
        {
            var order = GetValidOrder();
            order.ConstantSymbol = constantSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ConstantSymbol_Empty_Pass()
        {
            var order = GetValidOrder();
            order.ConstantSymbol = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { "1" })]
        [InlineData(new object[] { "12" })]
        [InlineData(new object[] { "123" })]
        [InlineData(new object[] { "12345" })]
        [InlineData(new object[] { "123456" })]
        [InlineData(new object[] { "abcd" })]
        public async Task ConstantSymbol_InvalidConstantSymbol_Fail(string constantSymbol)
        {
            var order = GetValidOrder();
            order.ConstantSymbol = constantSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.ConstantSymbol));
            result.Errors[0].ErrorCode.Should().Be("ConstantSymbolValidator");
        }

        [Theory]
        [InlineData(new object[] { "1" })]
        [InlineData(new object[] { "12" })]
        [InlineData(new object[] { "123" })]
        [InlineData(new object[] { "1234" })]
        [InlineData(new object[] { "12345" })]
        [InlineData(new object[] { "123456" })]
        [InlineData(new object[] { "1234567" })]
        [InlineData(new object[] { "12345678" })]
        [InlineData(new object[] { "123456789" })]
        [InlineData(new object[] { "1234567890" })]
        public async Task VariableSymbol_ValidVariableSymbol_Pass(string variableSymbol)
        {
            var order = GetValidOrder();
            order.VariableSymbol = variableSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task VariableSymbol_Empty_Pass()
        {
            var order = GetValidOrder();
            order.VariableSymbol = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { "12345678901" })]
        [InlineData(new object[] { "abcd" })]
        public async Task VariableSymbol_InvalidVariableSymbol_Fail(string variableSymbol)
        {
            var order = GetValidOrder();
            order.VariableSymbol = variableSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.VariableSymbol));
            result.Errors[0].ErrorCode.Should().Be("VariableSymbolValidator");
        }
        
        [Theory]
        [InlineData(new object[] { "1" })]
        [InlineData(new object[] { "12" })]
        [InlineData(new object[] { "123" })]
        [InlineData(new object[] { "1234" })]
        [InlineData(new object[] { "12345" })]
        [InlineData(new object[] { "123456" })]
        [InlineData(new object[] { "1234567" })]
        [InlineData(new object[] { "12345678" })]
        [InlineData(new object[] { "123456789" })]
        [InlineData(new object[] { "1234567890" })]
        public async Task SpecificSymbol_ValidSpecificSymbol_Pass(string specificSymbol)
        {
            var order = GetValidOrder();
            order.SpecificSymbol = specificSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task SpecificSymbol_Empty_Pass()
        {
            var order = GetValidOrder();
            order.SpecificSymbol = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { "12345678901" })]
        [InlineData(new object[] { "abcd" })]
        public async Task SpecificSymbol_InvalidSpecificSymbol_Fail(string specificSymbol)
        {
            var order = GetValidOrder();
            order.SpecificSymbol = specificSymbol;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.SpecificSymbol));
            result.Errors[0].ErrorCode.Should().Be("SpecificSymbolValidator");
        }

        [Theory]
        [InlineData(new object[] { "2020-10-05" })]
        public async Task Date_ValidDate_Pass(string date)
        {
            var order = GetValidOrder();
            order.Date = date;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task Date_Empty_Fail(string date)
        {
            var order = GetValidOrder();
            order.Date = date;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Date));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "20202-05-05" })]
        [InlineData(new object[] { "2020-09-31" })]
        [InlineData(new object[] { "2020-00-01" })]
        [InlineData(new object[] { "2020-13-01" })]
        [InlineData(new object[] { "2020-02-30" })]
        [InlineData(new object[] { "2020-01-1" })]
        [InlineData(new object[] { "2020-1-01" })]
        [InlineData(new object[] { "2020-1-1" })]
        [InlineData(new object[] { "wrongdate" })]
        public async Task Date_InvalidDate_Fail(string date)
        {
            var order = GetValidOrder();
            order.Date = date;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Date));
            result.Errors[0].ErrorCode.Should().Be("DateValidator");
        }

        [Fact]
        public async Task MessageForReceipient_ValidMessageForReceipient_Pass()
        {
            var order = GetValidOrder();
            order.MessageForRecipient = string.Create(140, 0, (p, s) => p.Fill('0'));

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task MessageForReceipient_Empty_Pass(string messageForReceipient)
        {
            var order = GetValidOrder();
            order.MessageForRecipient = messageForReceipient;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task MessageForReceipient_InvalidMessageForReceipient_Fail()
        {
            var order = GetValidOrder();
            order.MessageForRecipient = string.Create(141, 0, (p, s) => p.Fill('0'));

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.MessageForRecipient));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task Comment_ValidComment_Pass()
        {
            var order = GetValidOrder();
            order.Comment = string.Create(255, 0, (p, s) => p.Fill('0'));

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task Comment_Empty_Pass(string comment)
        {
            var order = GetValidOrder();
            order.Comment = comment;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Comment_InvalidComment_Fail()
        {
            var order = GetValidOrder();
            order.Comment = string.Create(256, 0, (p, s) => p.Fill('0'));

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Comment));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task PaymentReason_ValidPaymentReason_Pass()
        {
            var order = GetValidOrder();
            order.PaymentReason = PaymentReason.Reason110;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task PaymentReason_Empty_Pass()
        {
            var order = GetValidOrder();
            order.PaymentReason = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task PaymentReason_InvalidPaymentReason_Fail()
        {
            var order = GetValidOrder();
            order.PaymentReason = (PaymentReason)1;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.PaymentReason));
            result.Errors[0].ErrorCode.Should().Be("PaymentReasonValidator");
        }

        [Theory]
        [InlineData(new object[] { PaymentType.Standard })]
        [InlineData(new object[] { PaymentType.Precedential })]
        [InlineData(new object[] { PaymentType.Collection })]
        public async Task PaymentType_ValidPaymentType_Pass(PaymentType paymentType)
        {
            var order = GetValidOrder();
            order.PaymentType = paymentType;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task PaymentType_Empty_Pass()
        {
            var order = GetValidOrder();
            order.PaymentType = null;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task PaymentType_InvalidPaymentType_Fail()
        {
            var order = GetValidOrder();
            order.PaymentType = (PaymentType)1;

            var result = await new DomesticTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.PaymentType));
            result.Errors[0].ErrorCode.Should().Be("PaymentTypeValidator");
        }
    }
}
