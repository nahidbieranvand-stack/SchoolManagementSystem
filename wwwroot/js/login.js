document.addEventListener("DOMContentLoaded", function () {

    const passwordInput = document.getElementById("passwordInput");
    const togglePassword = document.getElementById("togglePassword");

    if (passwordInput && togglePassword) {

        togglePassword.addEventListener("click", function () {

            const icon = togglePassword.querySelector("i");

            if (passwordInput.type === "password") {

                passwordInput.type = "text";
                icon.classList.remove("bi-eye");
                icon.classList.add("bi-eye-slash");

            } else {

                passwordInput.type = "password";
                icon.classList.remove("bi-eye-slash");
                icon.classList.add("bi-eye");

            }

        });
    }
});


async function refreshCaptcha() {

    try {

        const response = await fetch("/Account/RefreshCaptcha");

        if (!response.ok) {
            throw new Error("خطا در دریافت کپچا");
        }

        const data = await response.json();

        const captchaCode = document.querySelector(".captcha-code");

        if (captchaCode) {
            captchaCode.textContent = data.captchaCode;
        }

    }
    catch (error) {

        console.error("Captcha Error:", error);

    }
   
}
async function refreshCaptcha() {
    try {
        const response = await fetch('/Account/RefreshCaptcha');

        if (!response.ok) {
            throw new Error('خطا در دریافت کپچا');
        }

        const data = await response.json();

        document.querySelector('.captcha-code').textContent = data.captchaCode;
    }
    catch (error) {
        console.error(error);
    }
}