const form = document.querySelector("#register-form"),
    emailField = form.querySelector(".email-error"),
    emailInput = document.querySelector("#email")
usernameField = form.querySelector(".username-error"),
    usernameInput = document.querySelector("#surname")
passField = form.querySelector(".password-error"),
    passInput = document.querySelector("#password"),
    cPassField = form.querySelector(".cPassword-error"),
    cPassInput = document.querySelector("#cpassword");

function checkUsername() {
    var nameRegex = /^[a-zA-Z\-]+$/;
    if (!usernameInput.value.match(nameRegex)) {
        return usernameField.classList.add("invalid");
    }
    usernameField.classList.remove("invalid");
}
// Email Validtion
function checkEmail() {
    const emaiPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emaiPattern)) {
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
function createPass() {
    const passPattern =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

    if (!passInput.value.match(passPattern)) {
        return passField.classList.add("invalid");
    }
    passField.classList.remove("invalid");
}

// Confirm Password Validtion
function confirmPass() {
    if (passInput.value !== cPassInput.value || cPassInput.value == "") {
        return cPassField.classList.add("invalid");
    }
    cPassField.classList.remove("invalid");
}


form.addEventListener("submit", (e) => {
    e.preventDefault();
    checkUsername()
    checkEmail();
    createPass();
    confirmPass();

    emailInput.addEventListener("keyup", checkEmail);
    usernameInput.addEventListener("keyup", checkUsername);
    passInput.addEventListener("keyup", createPass);
    cPassInput.addEventListener("keyup", confirmPass);
});
