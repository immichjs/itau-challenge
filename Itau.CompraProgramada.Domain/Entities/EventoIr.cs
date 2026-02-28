using Itau.CompraProgramada.Domain.Enums;

namespace Itau.CompraProgramada.Domain.Entities;

public class EventoIr
{
    public long Id { get; private set; }
    public long ClienteId { get; private set; }
    public ETipoEventoIr Tipo { get; private set; }

    public decimal ValorBase { get; private set; }
    public decimal ValorIr { get; private set; }
    public bool PublicadoKafka { get; private set; }
    public DateTime DataEvento { get; private set; }

    private EventoIr() { }

    public EventoIr(long clienteId, ETipoEventoIr tipo, decimal valorBase, decimal valorIr, DateTime dataEvento)
    {
        if (clienteId <= 0) throw new ArgumentOutOfRangeException(nameof(clienteId));
        if (valorBase < 0) throw new ArgumentOutOfRangeException(nameof(valorBase));
        if (valorIr < 0) throw new ArgumentOutOfRangeException(nameof(valorIr));

        ClienteId = clienteId;
        Tipo = tipo;
        ValorBase = valorBase;
        ValorIr = valorIr;
        PublicadoKafka = false;
        DataEvento = dataEvento;
    }

    public void MarcarComoPublicado()
    {
        PublicadoKafka = true;
    }
}