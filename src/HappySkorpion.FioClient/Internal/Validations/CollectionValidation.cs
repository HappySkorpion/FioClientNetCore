using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel;

namespace HappySkorpion.FioClient.Internal.Validations
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class CollectionValidation<TItem, TItemValidator>
        : AbstractValidator<IEnumerable<TItem>>
        where TItemValidator : IValidator<TItem>, new()
    {
        public CollectionValidation()
        {
            RuleForEach(items => items)
                .SetValidator(new TItemValidator());
        }
    }
}
