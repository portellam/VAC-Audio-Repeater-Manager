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

        /// <summary>
        /// Set the arrow color to the color table fore color.
        /// </summary>
        /// <param name="toolStripArrowRenderEventArgs">The tool strip arrow render
        /// event arguments</param>
        protected override void OnRenderArrow
            (ToolStripArrowRenderEventArgs toolStripArrowRenderEventArgs)
        {
            toolStripArrowRenderEventArgs.ArrowColor = darkColorTable.ForeColor;
            base.OnRenderArrow(toolStripArrowRenderEventArgs);
        }
    }
}