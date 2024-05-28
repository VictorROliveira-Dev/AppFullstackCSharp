using AppFinancas.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppFinancas.Shared.Requests.Transactions;

public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "Título inválido.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo necessário.")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required(ErrorMessage = "Valor inválido, insira um valor válido.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Categoria inválida, insira uma categoria válida.")]
    public long CategoryId { get; set; }

    [Required(ErrorMessage = "Data inválida.")]
    public DateTime? PairOrReceivedAt { get; set; }
}
