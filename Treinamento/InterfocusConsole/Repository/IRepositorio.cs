namespace InterfocusConsole.Repository
{
    public interface IRepository
    {
        void Incluir(object model);
        void Salvar(object model);
        void Excluir(object model);
        T ConsultarPorId<T>(long id);
        IQueryable<T> Consultar<T>();
    }
}
