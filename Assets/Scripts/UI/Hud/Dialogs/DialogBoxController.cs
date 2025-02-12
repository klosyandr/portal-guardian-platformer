using UnityEngine;
using UnityEngine.UI;
using PortalGuardian.Model.Data;
using TMPro;
using System.Collections;
using System;

namespace PortalGuardian.UI
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")]
        [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;

        private void Start()
        {
            _sfxSource = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentence = 0;
            _text.text = string.Empty;

            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(AnimatorKeys.IS_OPEN, true);
        }

        private IEnumerator TypingDialogText()
        {
            _text.text = string.Empty;
            var sentence = _data.Sentences[_currentSentence];
            foreach (var letter in sentence)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }
        
        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
            }
            _typingRoutine = null;
        }
        
        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogCompleted = _currentSentence >= _data.Sentences.Length;
            if (isDialogCompleted)
            {
                HideDialogBox();

            }
            else
            {
                OnStartDialogAnimation();
            }

        }

        private void HideDialogBox()
        {
            _sfxSource.PlayOneShot(_close);
            _animator.SetBool(AnimatorKeys.IS_OPEN, false);
        }

        private void OnStartDialogAnimation()
        {
            _typingRoutine = StartCoroutine(TypingDialogText());
        }

        private void OnCloseAnimationComplete()
        {
            
            _container.SetActive(false);
        }
    }
}