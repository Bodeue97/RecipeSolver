export function validateEmail(email) {
    const emailRegex = /\S+@\S+\.\S+/;
    return emailRegex.test(email);
  }
  
  export function validatePassword(password) {
    return password.length >= 8;
  }

  export function validateConfirmPassword(password, confirmPassword) {
    return password === confirmPassword;
  }
  