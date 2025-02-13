using System;
using PortalGuardian.Model.Data;
using PortalGuardian.UI;
using UnityEngine;

namespace PortalGuardian.Component.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        private DialogBoxController _dialogBox;
        public void Show()
        {
            if (_dialogBox == null)
            {
                _dialogBox = FindObjectOfType<DialogBoxController>();
            }
            _dialogBox.ShowDialog(Data);
        }

        public DialogData Data
        {
            get 
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}