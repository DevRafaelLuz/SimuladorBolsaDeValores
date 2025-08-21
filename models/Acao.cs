public class Acao
{
    private string empresa;
    private string codigo;
    private double preco;
    private double variacaoMaxima;
    private double variacaoMinima;

    public String getEmpresa()
    {
        return empresa;
    }

    public void setEmpresa(String empresa)
    {
        this.empresa = empresa;
    }

    public String getCodigo()
    {
        return codigo;
    }

    public void setCodigo(String codigo)
    {
        this.codigo = codigo;
    }

    public double getPreco()
    {
        return preco;
    }

    public void setPreco(double preco)
    {
        this.preco = preco;
    }

    public double getVariacaoMaxima()
    {
        return variacaoMaxima;
    }

    public void setVariacaoMaxima(double variacaoMaxima)
    {
        this.variacaoMaxima = variacaoMaxima;
    }

    public double getVariacaoMinima()
    {
        return variacaoMinima;
    }

    public void setVariacaoMinima(double variacaoMinima)
    {
        this.variacaoMinima = variacaoMinima;
    }
}