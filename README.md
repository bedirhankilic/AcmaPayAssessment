#### Authorization request
#### POST /api/authorize/
Request Model
```json
{
    "Amount" : 125.2,
    "Currency" : "EUR",
    "CardHolder" : "1234567812345678",
    "HolderName" : "Bedirhan Kilic",
    "ExpirationMonth" : 5,
    "ExpirationYear" : 2020,
    "CVV" :120,
    "OrderReference" : "s"
}

```

Response Model
```json
{
    "id": "a47edcca-c422-4e17-a798-52393eb48aa3",
    "status": 0
}
```

#### Void request
#### POST /api/authorize/{id}/voids
Request Model
```json
{
    "OrderReference" : "s"
}

```
Response Model
```json
{
    "id": "4e58cbae-0eb0-4ead-85a2-aba0b09e8606",
    "status": 2
}
```

#### Capture request
#### POST /api/authorize/{id}/capture
Request Model
```json
{
    "OrderReference" : "s"
}

```
Response Model
```json
{
    "id": "4e58cbae-0eb0-4ead-85a2-aba0b09e8606",
    "status": 1
}
```

#### Get all transactions
#### GET /api/authorize/

Response Model
```json
[
    {
        "id": "84317aaf-a4fd-4e3d-8d20-c27015c28ed4",
        "paymentId": "a47edcca-c422-4e17-a798-52393eb48aa3",
        "amount": 125.20,
        "currency": "EUR",
        "cardHolder": "123456******5678",
        "holderName": "Bedirhan Kilic",
        "orderReference": "s",
        "status": 0
    },
    {
        "id": "3855eeab-b4a1-430a-8356-eda0bcbbd2a9",
        "paymentId": "4e58cbae-0eb0-4ead-85a2-aba0b09e8606",
        "amount": 12335.99,
        "currency": "EUR",
        "cardHolder": "123456******5678",
        "holderName": "Bedirhan Kilic",
        "orderReference": "asd",
        "status": 1
    }
]
```
