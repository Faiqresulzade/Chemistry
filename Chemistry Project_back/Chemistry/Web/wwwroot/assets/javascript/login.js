const form = document.querySelector("#login-form"),
    emailField = form.querySelector(".email-login-error"),
    emailInput = document.querySelector("#email")
passwordField = form.querySelector(".password-login-error"),
    passwordInput = document.querySelector("#password")


// Email Validation
function checkLoginEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emailPattern)) {
        return emailField.classList.add("invalid");
    }
    emailField.classList.remove("invalid");
}

// Hide and show password
const eyeIcons = document.querySelectorAll(".show-hide");

eyeIcons.forEach((eyeIcon) => {
    eyeIcon.addEventListener("click", () => {
        const pInput = eyeIcon.parentElement.querySelector("input");
        if (pInput.type === "password") {
            eyeIcon.classList.replace("bx-hide", "bx-show");
            return (pInput.type = "text");
        }
        eyeIcon.classList.replace("bx-show", "bx-hide");
        pInput.type = "password";
    });
});

// Password Validation
function addPass() {
    if (passwordInput.value == "") {
        return passwordField.classList.add("invalid");
    }
    passwordField.classList.remove("invalid");

}


form.addEventListener("submit", (e) => {
    e.preventDefault();
    addPass()
    checkLoginEmail()
    emailInput.addEventListener("keyup", checkLoginEmail);
    passwordInput.addEventListener("keyup", addPass);
});
