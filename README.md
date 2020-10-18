# Fio Client

This repository contains codes for **Fio Client** which wraps communication with Fio bank account through Fio API.

Implementation is based on [Fio API Documentation version 1.6.27](https://www.fio.sk/docs/cz/API_Bankovnictvi.pdf).

## Fio API token

Fio Client requires for communication with Fio API a token. 

The token can be generated through [API settings page](https://ib.fio.sk/ib/wicket/page/NastaveniPage?4) after login into Fio internetbanking.

## Examples 

### Creating Fio client

```C#
// Some Fio API token
var token = "v1Xo3S3SemJ...";

// Create Fio client
var client = new ApiClient(token);
```

### Listing bank account transactions

```C#
// List all transactions in specified period
var result = await client.ListTransactionsAsync(DateTime.Today.AddDays(-1), DateTime.Today);

// List all transactions on first extraction in year 2019
var result = await client.ListTransactionsAsync(2019, 1);

// List all transactions since last transactions download or backstop
var result = await client.ListLastTransactionsAsync();
```

### Setting the backstop

```C#
// Set the backstop to some date
await client.SetLastTransactionAsync(new DateTime(2020, 9, 1));

// Set the backstop to some transaction id
await client.SetLastTransactionAsync(12548777);
```

### Sending transaction orders

```C#
// Send domestic transaction orders
await client.SendTransactionOrdersAsync(new [] {
  new DomesticTransactionOrder
  {
    SourceAccountNumber = "1234567890",
    Amount = 100,
    Currency = CurrencyCode.CZK,
    DestinationAccountBank = "8330",
    DestinationAccountNumber = "000000-0987654321",
    ConstantSymbol = "0123",
    VariableSymbol = "0000123456",
    SpecificSymbol = "0000654321",
    Date = "2020-10-20",
    MessageForRecipient = "Some message for recipient",
    Comment = "Fio transaction test",
    PaymentReason = PaymentReason.Reason110,
    PaymentType = PaymentType.Standard,
  }
});

// Send euro transaction orders
await client.SendTransactionOrdersAsync(new [] {
  new DomesticTransactionOrder
  {
    SourceAccountNumber = "1234567890",
    Amount = 100,
    Currency = CurrencyCode.EUR,
    DestinationAccountNumber = "SK31 1200 0000 1987 4263 7541",
    ConstantSymbol = "0123",
    VariableSymbol = "0000123456",
    SpecificSymbol = "0000654321",
    Bic = "IBCLUS44",
    Date = "2020-10-21",
    Comment = "Fio transaction test",
    BenefName = "John Smith",
    BenefStreet = "Happy street 1",
    BenefCity = "London",
    BenefCountry = "GB",
    RemittanceInfo1 = "Remittance info",
    PaymentReason = PaymentReason.Reason110,
    PaymentType = PaymentType.Standard,
  }
});

// Send foreign transaction orders
await client.SendTransactionOrdersAsync(new [] {
  new ForeignTransactionOrder
  {
    SourceAccountNumber = "1234567890",
    Amount = 100,
    Currency = CurrencyCode.USD,
    DestinationAccountNumber = "SK31 1200 0000 1987 4263 7541",
    Bic = "IBCLUS44",
    Date = "2020-05-22",
    Comment = "Fio transaction test",
    BenefName = "John Smith",
    BenefStreet = "Happy street 1",
    BenefCity = "London",
    BenefCountry = "GB",
    RemittanceInfo1 = "Remittance info",
    DetailsOfCharges = ChargeType.SHA,
    PaymentReason = PaymentReason.Reason110,
  }
});
```

### Signing sent transaction orders

The sent transaction orders are not authorized.
They are only created and preppared for signing.
The signing 
needs to be done through Fio internet banking.

## Bugs

Pls, let me know if you find some bug.
