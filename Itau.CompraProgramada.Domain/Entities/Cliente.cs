namespace Itau.CompraProgramada.Domain.Entities;

public class Cliente
{
    public long Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public decimal ValorMensal { get; private set; }
    public bool Ativo { get; private set; } = true;
    public DateTime DataAdesao { get; private set; }

    private Cliente() { }

    public Cliente(string nome, string cpf, string email, decimal valorMensal)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF é obrigatório.", nameof(cpf));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.", nameof(email));
        if (valorMensal <= 0) throw new ArgumentOutOfRangeException(nameof(valorMensal), "Valor mensal deve ser maior que zero.");

        Nome = nome.Trim();
        Cpf = cpf.Trim();
        Email = email.Trim();
        ValorMensal = valorMensal;

        Ativo = true;
        DataAdesao = DateTime.UtcNow;
    }

    public void AlterarValorMensal(decimal novoValor)
    {
        if (novoValor <= 0) throw new ArgumentOutOfRangeException(nameof(novoValor));
        ValorMensal = novoValor;
    }

    public void SairDoProduto()
    {
        Ativo = false;
    }
}