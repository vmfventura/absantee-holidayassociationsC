using Domain.Factory;

namespace Domain.Model;

public class Associations
{
    public IAssociationFactory _associationFactory;
    private List<IAssociation> _associationsList = new List<IAssociation>();

    public Associations()
    {
    }

    public Associations(IAssociationFactory aFactory)
    {
        if (aFactory is not null)
        {
            _associationFactory = aFactory;
        }
        else
        {
            throw new ArgumentException("Invalid argument: associationFactory must be non null");
        }
    }
    
    public Get
}