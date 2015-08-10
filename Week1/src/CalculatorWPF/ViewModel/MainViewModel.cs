using System;
using System.Globalization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CalculatorWPF.ViewModel
{
    public enum Operation
    {
        Add,
        Subtract,
        Divide,
        Multiply
    }
    public class MainViewModel : ViewModelBase
    {
        private Operation _chosenOperation;
        private double _First;

        private double _Second;

        private bool _IsFirstNumber;

        private int _NumberOfOperation;

        private bool _IncludeFractial;

        private bool _FractialAlreadyIncluded;
        

        #region Properties

        private string _ResultToShow;
        public string ResultToShow
        {
            get
            {
                return _ResultToShow;
            }
            set
            {
                _ResultToShow = value;
                RaisePropertyChanged(() => ResultToShow);
            }
        }
        #endregion

        public MainViewModel()
        {
            _First = 0;
            _Second = 0;
            _IsFirstNumber = true;
            _NumberOfOperation = 0;
            _IncludeFractial = false;
            _FractialAlreadyIncluded = false;
            ResultToShow = _First.ToString();
            InitializeCommands();
        }

        #region Commands

        public RelayCommand AddCommand { get; set; }
        public RelayCommand SubtractCommand { get; set; }
        public RelayCommand DivideCommand { get; set; }
        public RelayCommand MultiplyCommand { get; set; }
        public RelayCommand<object> EnterNumberCommand { get; set; }
        public RelayCommand EqualCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand FractialCommand { get; set; }
        public RelayCommand ChangeSignCommand { get; set; }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(OnAddCommand);
            SubtractCommand = new RelayCommand(OnSubtractCommand);
            DivideCommand = new RelayCommand(OnDivideCommand);
            MultiplyCommand = new RelayCommand(OnMultiplyCommand);
            EnterNumberCommand = new RelayCommand<object>(OnEnterNumberCommand);
            EqualCommand = new RelayCommand(OnEqualCommand);
            ClearCommand = new RelayCommand(OnClearCommand);
            FractialCommand = new RelayCommand(OnFractialCommand);
            ChangeSignCommand = new RelayCommand(OnChangeSignCommand);
        }

        private void OnChangeSignCommand()
        {
            if (_IsFirstNumber)
            {
                _First = -_First;
                ResultToShow = _First.ToString();
            }
            else
            {
                _Second = -_Second;
                ResultToShow = _Second.ToString();
            }
        }

        private void OnFractialCommand()
        {
            if (_IsFirstNumber)
            {
                ResultToShow = _First.ToString() + ".";
                _IncludeFractial = true;
            }
            else
            {
                ResultToShow = _Second.ToString() + ".";
                _IncludeFractial = true;
            }
        }

        private void OnEqualCommand()
        {
            switch (_chosenOperation)
            {
                case Operation.Add:
                {
                    _First = _First + _Second;
                    _Second = 0;
                    ResultToShow = _First.ToString();
                    break;
                }
                case Operation.Subtract :
                {
                    _First = _First - _Second;
                    _Second = 0;
                    ResultToShow = _First.ToString();
                    break;
                }
                case Operation.Multiply:
                {
                    _First = _First * _Second;
                    _Second = 0;
                    ResultToShow = _First.ToString();
                    break;
                }
                case Operation.Divide:
                {
                    if (_Second.Equals(0.0))
                    {
                        ResultToShow = "Cannot divide by zero!";
                        OnClearCommand();
                    }
                    else
                    {
                        _First = _First / _Second;
                        _Second = 0;
                        ResultToShow = _First.ToString();
                    }                   
                    break;
                }
                    

            }
        }

        private void OnEnterNumberCommand(object number)
        {
            if (_IsFirstNumber)
            {
                if (_IncludeFractial && !_FractialAlreadyIncluded)
                {
                    _First = _First + Double.Parse(number.ToString())/10;
                    _IncludeFractial = false;
                    _FractialAlreadyIncluded = true;
                }
                else
                {
                    _First = Double.Parse(_First.ToString() + number.ToString());
                }
                ResultToShow = _First.ToString();
            }
            else
            {
                if (_IncludeFractial && !_FractialAlreadyIncluded)
                {
                    _Second = _Second + Double.Parse(number.ToString())/10;
                    _IncludeFractial = false;
                    _FractialAlreadyIncluded = true;
                }
                else
                {
                    _Second = Double.Parse(_Second.ToString() + number.ToString());    
                }
                ResultToShow = _Second.ToString();
            }
            
        }

        private void OnMultiplyCommand()
        {
            _FractialAlreadyIncluded = false;
            _NumberOfOperation++;
            if (_NumberOfOperation > 1)
            {
                OnEqualCommand();
            }
            _chosenOperation = Operation.Multiply;
            _IsFirstNumber = false;            
        }

        private void OnDivideCommand()
        {
            _FractialAlreadyIncluded = false;
            _NumberOfOperation++;
            if (_NumberOfOperation > 1)
            {
                OnEqualCommand();
            }
            _chosenOperation = Operation.Divide;
            _IsFirstNumber = false;
           
        }

        private void OnSubtractCommand()
        {
            _FractialAlreadyIncluded = false;
            _NumberOfOperation++;
            if (_NumberOfOperation > 1)
            {
                OnEqualCommand();
            }
            _chosenOperation = Operation.Subtract;
            _IsFirstNumber = false;           
        }

        private void OnAddCommand()
        {
            _FractialAlreadyIncluded = false;
            _NumberOfOperation++;
            if (_NumberOfOperation > 1)
            {
                OnEqualCommand();
            }
            _chosenOperation = Operation.Add;
            _IsFirstNumber = false;           
        }

        private void OnClearCommand()
        {
            _FractialAlreadyIncluded = false;
            _IncludeFractial = false;
            _IsFirstNumber = true;
            _First = 0;
            _Second = 0;
            _NumberOfOperation = 0;
        }

        #endregion
    }
}