using Itau.CompraProgramada.Domain.Enums;

namespace Itau.CompraProgramada.Domain.Entities;

public class ContaGrafica
{
    public long Id { get; private set; }
    public long? ClienteId { get; private set; } 
    public string NumeroConta { get; private set; } = null!;
    public ETipoConta Tipo { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private ContaGrafica() { }

    public ContaGrafica(string numeroConta, ETipoConta tipo, long? clienteId)
    {
        if (string.IsNullOrWhiteSpace(numeroConta)) throw new ArgumentException("NúmeroConta é obrigatório.", nameof(numeroConta));

        NumeroConta = numeroConta.Trim();
        Tipo = tipo;
        ClienteId = clienteId;
        DataCriacao = DateTime.UtcNow;
    }
}