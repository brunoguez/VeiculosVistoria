namespace VistoriaAPI.Veiculos
{
    public record AddVeiculoRequest(string? Placa, string? Chassi, string? Motor,
        int Ano_Fabricacao, int Ano_Modelo, string? Marca, string? Linha,
        string? Descricao, double? Potencia, string? Observacoes,
        DateTime? DataIntegracao)
    {
        public Veiculo ConvertToVeiculo(Veiculo? old = null)
        {
            Veiculo veiculo = old is null ? new Veiculo() : old;
            veiculo.Placa = Placa;
            veiculo.Chassi = Chassi;
            veiculo.Motor = Motor;
            veiculo.Ano_Fabricacao = Ano_Fabricacao;
            veiculo.Ano_Modelo = Ano_Modelo;
            veiculo.Marca = Marca;
            veiculo.Linha = Linha;
            veiculo.Descricao = Descricao;
            veiculo.Potencia = Potencia;
            veiculo.Observacoes = Observacoes;
            veiculo.DataIntegracao = DataIntegracao;
            return veiculo;
        }
    };
}
