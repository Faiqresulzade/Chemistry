// !loading
const loader = document.querySelector('#preloader');
window.addEventListener('load', () => {
  loader.style.display = 'none';
});
// ! Hide and show password
const eyeIcons = document.querySelectorAll('.show-hide');

eyeIcons.forEach((eyeIcon) => {
  eyeIcon.addEventListener('click', () => {
    const pInput = eyeIcon.parentElement.querySelector('input');
    if (pInput.type === 'password') {
      eyeIcon.classList.replace('bx-hide', 'bx-show');
      return (pInput.type = 'text');
    }
    eyeIcon.classList.replace('bx-show', 'bx-hide');
    pInput.type = 'password';
  });
});