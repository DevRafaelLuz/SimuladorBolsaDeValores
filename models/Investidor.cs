public class Investidor
{
    private string nome;
    private int idade;
    private string cpf;
    private string email;
    private string senha;

    public Investidor(string nome, int idade, string cpf, string email, string senha)
    {
        this.nome = nome;
        this.idade = idade;
        this.cpf = cpf;
        this.email = email;
        this.senha = senha;
    }

    public string getNome()
    {
        return nome;
    }

    public void setNome(string nome)
    {
        this.nome = nome;
    }

    public void setIdade(int idade)
    {
        this.idade = idade;
    }

    public string getCpf()
    {
        return cpf;
    }

    public void setCpf(string cpf)
    {
        this.cpf = cpf;
    }

    public string getEmail()
    {
        return email;
    }

    public void setEmail(string email)
    {
        this.email = email;
    }

    public string getSenha()
    {
        return senha;
    }

    public void setSenha(string senha)
    {
        this.senha = senha;
    }
}