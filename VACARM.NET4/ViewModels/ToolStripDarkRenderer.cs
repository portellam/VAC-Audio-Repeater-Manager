using System.Windows.Forms;

namespace VACARM.NET4.ViewModels
{
	public class ToolStripDarkRenderer : ToolStripProfessionalRenderer
	{
		private readonly DarkColorTable darkColorTable = new DarkColorTable();

		public ToolStripProfessionalRenderer ToolStripProfessionalRenderer =>
			new ToolStripProfessionalRenderer(darkColorTable);

		public new ProfessionalColorTable ColorTable
		{
			get
			{
				return darkColorTable;
			}
		}

		protected override void OnRenderArrow
			(ToolStripArrowRenderEventArgs toolStripArrowRenderEventArgs)
		{
			toolStripArrowRenderEventArgs.ArrowColor = darkColorTable.ForeColor;
			base.OnRenderArrow(toolStripArrowRenderEventArgs);
		}
	}
}