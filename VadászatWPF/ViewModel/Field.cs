using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VadászatWPF.ViewModel
{
    public class Field : ViewModelBase
    {
        public int X { get; set; }

        public int Y { get; set; }


        private string? text;
        public string Text
        {
            get { return text ?? string.Empty; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }
        private int isHidden;
        public int IsHidden
        {
            get { return isHidden; }
            set
            {
                if (isHidden != value)
                {
                    isHidden = value;
                    OnPropertyChanged();
                }
            }
        }
        public DelegateCommand? ButtonClick { get; set; }
        public Tuple<Int32, Int32> XY
        {
            get { return new(X, Y); }
        }

        private int character;

        public int Character
        {
            get { return character; }
            set
            {
                if (character != value)
                {
                    character = value;
                    OnPropertyChanged(nameof(Character));
                }
            }
        }
    }
}
