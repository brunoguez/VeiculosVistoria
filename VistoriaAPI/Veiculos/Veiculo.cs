using Microsoft.EntityFrameworkCore;

namespace VistoriaAPI.Veiculos
{
    [Index(nameof(Placa), nameof(Chassi), nameof(Motor))]
    public class Veiculo
    {
        public int Id { get; set; }
        public string? Placa { get; set; }
        public string? Chassi { get; set; }
        public string? Motor { get; set; }
        public int Ano_Fabricacao { get; set; }
        public int Ano_Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Linha { get; set; }
        public string? Descricao { get; set; }
        public double? Potencia { get; set; }
        public string? Observacoes { get; set; }
        public DateTime? DataIntegracao { get; set; }
    }
}