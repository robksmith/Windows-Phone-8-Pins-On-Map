#region Usings

using System.Windows.Controls;

#endregion


namespace Zengo.PinsOnMap.App.Controls
{
    /// <summary>
    /// Encapsulates a pin to be drawn - as per the instructions it is a pin with text ( timestamp )
    /// </summary>
    public partial class PinOnMap : UserControl
    {
        #region Constructors

        public PinOnMap()
        {
            InitializeComponent();
        }

        #endregion


        #region Properties

        /// <summary>
        /// Expose a property to allow the containing page to inject the name of the pin
        /// </summary>
        public string Text
        {
            get
            {
                return TextPin.Text;
            }
            set
            {
                TextPin.Text = value;
            }
        }

        #endregion
    }
}
