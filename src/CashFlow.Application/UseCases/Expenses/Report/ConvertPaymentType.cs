using CashFlow.Domain.Enums;

public static class ConvertPayment
{
    public static string Convert(PaymentType payment)
    {
        return payment switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de crédito",
            PaymentType.DebitCard => "Cartão de débito",
            PaymentType.EletronicTransfer => "Transferência eletrônica",
            _ => string.Empty
        };
    }
}