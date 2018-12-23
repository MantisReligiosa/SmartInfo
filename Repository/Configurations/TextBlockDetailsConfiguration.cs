using DomainObjects.Blocks.Details;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Configurations
{
    public class TextBlockDetailsConfiguration : EntityTypeConfiguration<TextBlockDetails>
    {
        public TextBlockDetailsConfiguration()
        {
            ToTable("TextBlockDetails");
            HasKey(t => t.Id);
            Property(t => t.Text);
            Property(t => t.BackColor);
            Property(t => t.TextColor);
            Property(t => t.FontName);
            Property(t => t.FontSize);
            Property(t => t.Align);
        }
    }
}
