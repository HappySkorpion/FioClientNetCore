using FluentAssertions;
using HappySkorpion.FioClient.Internal.Validations;
using System.Threading.Tasks;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Unit.Validators
{
    [Trait("Category", "Unit")]
    public class ForeignTransactionOrderValidationTests
    {
        private ForeignTransactionOrder GetValidOrder()
        {
            return new ForeignTransactionOrder
            {
                SourceAccountNumber = "123456798",
                Amount = 100,
                Currency = CurrencyCode.EUR,
                DestinationAccountNumber = "SE35 5000 0000 0549 1000 0003",
                Bic = "DABAIE2D",
                Date = "2020-05-10",
                Comment = "Comment",
                BenefName = "BenefName",
                BenefStreet = "BenefStreet",
                BenefCity = "BenefCity",
                BenefCountry = "CZ",
                RemittanceInfo1 = "RemittanceInfo1",
                RemittanceInfo2 = "RemittanceInfo2",
                RemittanceInfo3 = "RemittanceInfo3",
                RemittanceInfo4 = "RemittanceInfo4",
                DetailsOfCharges = ChargeType.BEN,
                PaymentReason = PaymentReason.Reason110,
            };
        }

        [Fact]
        public async Task ValidOrder_Pass()
        {
            var order = GetValidOrder();

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task SourceAccountNumber_Empty_Fail()
        {
            var order = GetValidOrder();
            order.SourceAccountNumber = null;

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Amount_ZeroNumber_Fail()
        {
            var order = GetValidOrder();
            order.Amount = 0;

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Amount));
            result.Errors[0].ErrorCode.Should().Be("GreaterThanValidator");
        }

        [Fact]
        public async Task Currency_Euro_Pass()
        {
            var order = GetValidOrder();
            order.Currency = CurrencyCode.EUR;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Currency_Empty_Fail()
        {
            var order = GetValidOrder();
            order.Currency = default;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Currency));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Fact]
        public async Task Currency_NotEuro_Fail()
        {
            var order = GetValidOrder();
            order.Currency = (CurrencyCode)1000;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Currency));
            result.Errors[0].ErrorCode.Should().Be("CurrencyValidator");
        }

        [Theory]
        [InlineData(new object[] { "SE35 5000 0000 0549 1000 0003" })]
        [InlineData(new object[] { "SE3550000000054910000003" })]

        public async Task DestinationAccountNumber_ValidIban_Pass(string accountTo)
        {
            var order = GetValidOrder();
            order.DestinationAccountNumber = accountTo;

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "SE35 5000 0000 0549 1000 0004" })]
        [InlineData(new object[] { "SE34 5000 0000 0549 1000 0003" })]
        [InlineData(new object[] { "SF35 5000 0000 0549 1000 0003" })]
        [InlineData(new object[] { "SF355000000054910000003" })]
        public async Task DestinationAccountNumber_InvalidIban_Fail(string accountTo)
        {
            var order = GetValidOrder();
            order.DestinationAccountNumber = accountTo;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DestinationAccountNumber));
            result.Errors[0].ErrorCode.Should().Be("IbanValidator");
        }

        [Theory]
        [InlineData(new object[] { "CHASUS33" })]
        [InlineData(new object[] { "CHASUS33ABC" })]
        [InlineData(new object[] { "BOFAUS3N" })]
        [InlineData(new object[] { "MIDLGB22" })]
        [InlineData(new object[] { "BARCGB22" })]
        public async Task Bic_ValidBic_Pass(string bic)
        {
            var order = GetValidOrder();
            order.Bic = bic;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Bic_Empty_Fail()
        {
            var order = GetValidOrder();
            order.Bic = null;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Bic));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Theory]
        [InlineData(new object[] { "0AAAAA00" })]
        [InlineData(new object[] { "A0AAAA00" })]
        [InlineData(new object[] { "AA0AAA00" })]
        [InlineData(new object[] { "AAA0AA00" })]
        [InlineData(new object[] { "AAAA0A00" })]
        [InlineData(new object[] { "AAAAA000" })]
        [InlineData(new object[] { "AAAAAA00A" })]
        [InlineData(new object[] { "AAAAAA00AA" })]
        public async Task Bic_InvalidBic_Fail(string bic)
        {
            var order = GetValidOrder();
            order.Bic = bic;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Bic));
            result.Errors[0].ErrorCode.Should().Be("BicValidator");
        }

        [Theory]
        [InlineData(new object[] { "2020-10-05" })]
        public async Task Date_ValidDate_Pass(string date)
        {
            var order = GetValidOrder();
            order.Date = date;

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Date));
            result.Errors[0].ErrorCode.Should().Be("DateValidator");
        }

        [Fact]
        public async Task Comment_ValidComment_Pass()
        {
            var order = GetValidOrder();
            order.Comment = string.Create(140, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
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

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Comment_InvalidComment_Fail()
        {
            var order = GetValidOrder();
            order.Comment = string.Create(141, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.Comment));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task BenefName_ValidBenefName_Pass()
        {
            var order = GetValidOrder();
            order.BenefName = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task BenefName_Empty_Fail(string benefName)
        {
            var order = GetValidOrder();
            order.BenefName = benefName;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.BenefName));
            result.Errors[0].ErrorCode.Should().Be("NotEmptyValidator");
        }

        [Fact]
        public async Task BenefName_InvalidBenefName_Fail()
        {
            var order = GetValidOrder();
            order.BenefName = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.BenefName));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task BenefStreet_ValidBenefStreet_Pass()
        {
            var order = GetValidOrder();
            order.BenefStreet = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task BenefStreet_Empty_Pass(string benefStreet)
        {
            var order = GetValidOrder();
            order.BenefStreet = benefStreet;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task BenefStreet_InvalidBenefStreet_Fail()
        {
            var order = GetValidOrder();
            order.BenefStreet = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.BenefStreet));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task BenefCity_ValidBenefCity_Pass()
        {
            var order = GetValidOrder();
            order.BenefCity = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task BenefCity_Empty_Pass(string benefCity)
        {
            var order = GetValidOrder();
            order.BenefCity = benefCity;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task BenefCity_InvalidBenefCity_Fail()
        {
            var order = GetValidOrder();
            order.BenefCity = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.BenefCity));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task BenefCountry_ValidBenefCountry_Pass()
        {
            var order = GetValidOrder();
            order.BenefCountry = string.Create(3, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task BenefCountry_Empty_Pass(string benefCountry)
        {
            var order = GetValidOrder();
            order.BenefCountry = benefCountry;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task BenefCountry_InvalidBenefCountry_Fail()
        {
            var order = GetValidOrder();
            order.BenefCountry = string.Create(4, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.BenefCountry));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task RemittanceInfo1_ValidRemittanceInfo1_Pass()
        {
            var order = GetValidOrder();
            order.RemittanceInfo1 = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task RemittanceInfo1_Empty_Pass(string remittanceInfo1)
        {
            var order = GetValidOrder();
            order.RemittanceInfo1 = remittanceInfo1;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task RemittanceInfo1_InvalidRemittanceInfo1_Fail()
        {
            var order = GetValidOrder();
            order.RemittanceInfo1 = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.RemittanceInfo1));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task RemittanceInfo2_ValidRemittanceInfo2_Pass()
        {
            var order = GetValidOrder();
            order.RemittanceInfo2 = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task RemittanceInfo2_Empty_Pass(string remittanceInfo2)
        {
            var order = GetValidOrder();
            order.RemittanceInfo2 = remittanceInfo2;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task RemittanceInfo2_InvalidRemittanceInfo2_Fail()
        {
            var order = GetValidOrder();
            order.RemittanceInfo2 = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.RemittanceInfo2));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task RemittanceInfo3_ValidRemittanceInfo3_Pass()
        {
            var order = GetValidOrder();
            order.RemittanceInfo3 = string.Create(35, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(new object[] { (string)null })]
        [InlineData(new object[] { "" })]
        public async Task RemittanceInfo3_Empty_Pass(string remittanceInfo3)
        {
            var order = GetValidOrder();
            order.RemittanceInfo3 = remittanceInfo3;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task RemittanceInfo3_InvalidRemittanceInfo3_Fail()
        {
            var order = GetValidOrder();
            order.RemittanceInfo3 = string.Create(36, 0, (p, s) => p.Fill('0'));

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.RemittanceInfo3));
            result.Errors[0].ErrorCode.Should().Be("MaximumLengthValidator");
        }

        [Fact]
        public async Task PaymentReason_ValidPaymentReason_Pass()
        {
            var order = GetValidOrder();
            order.PaymentReason = PaymentReason.Reason110;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task PaymentReason_InvalidPaymentReason_Fail()
        {
            var order = GetValidOrder();
            order.PaymentReason = (PaymentReason)1;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.PaymentReason));
            result.Errors[0].ErrorCode.Should().Be("PaymentReasonValidator");
        }

        [Theory]
        [InlineData(new object[] { ChargeType.BEN })]
        [InlineData(new object[] { ChargeType.OUR })]
        [InlineData(new object[] { ChargeType.SHA })]

        public async Task DetailsOfCharges_ValidDetailsOfCharges_Pass(ChargeType chargeType)
        {
            var order = GetValidOrder();
            order.DetailsOfCharges = chargeType;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task DetailsOfCharges_InvalidDetailsOfCharges_Fail()
        {
            var order = GetValidOrder();
            order.DetailsOfCharges = (ChargeType)1;

            var result = await new ForeignTransactionOrderValidation()
                .ValidateAsync(order)
                .ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.Errors[0].PropertyName.Should().Be(nameof(order.DetailsOfCharges));
            result.Errors[0].ErrorCode.Should().Be("ChargeTypeValidator");
        }
    }
}
