namespace DataModel.Mapper;

using DataModel.Model;

using Domain.Model;
using Domain.Factory;
using Domain.IRepository;

public class AssociationMapper
{
    private IAssociationFactory _associationFactory;

    public AssociationMapper(IAssociationFactory associationFactory)
    {
        _associationFactory = associationFactory;
    }

    public Association ToDomain(AssociationDataModel associationDM)
    {
        Association associationDomain = _associationFactory.NewAssociation(associationDM.ColaboratorId, associationDM.ProjectId, 
            associationDM.Period!.StartDate,associationDM.Period.EndDate);
        associationDomain.Id = associationDM.Id;
        return associationDomain;
    }

    public IEnumerable<Association> ToDomain(IEnumerable<AssociationDataModel> associacoesDM)
    {
        List<Association> associationsDomain = new List<Association>();

        foreach (AssociationDataModel associationDM in associacoesDM)
        {
            Association associationDomain = ToDomain(associationDM);

            associationsDomain.Add(associationDomain);
        }

        return associationsDomain.AsEnumerable();
    }

    public AssociationDataModel ToDataModel(Association Association)
    {
        AssociationDataModel associationDataModel = new AssociationDataModel(Association);

        return associationDataModel;
    }


    public bool UpdateDataModel(AssociationDataModel associationDataModel, Association associationDomain)
    {
        // pode ser necessário mais atualizações, e com isso o retorno não ser sempre true
        // contudo, porque associationDataModel está a ser gerido pelo DbContext, para atualizarmos a DB, é este que tem de ser alterado, e não criar um novo

        associationDataModel.Period.StartDate = associationDomain.StartDate;
        associationDataModel.Period.EndDate = associationDomain.EndDate;
        return true;
    }
}