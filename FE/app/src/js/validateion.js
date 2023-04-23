/**
 * Hàm validate dữ liệu
 * Created by NVTAN(04/04/2023)
 */
const validateData = (lstInput) => {
  var isValidate = true;
  if (lstInput) {
    for (var i = 0; i < lstInput.length; i++) {
      var component = lstInput[i].__vueParentComponent;
      if (component.props.rules) {
        let validate = validateInput(lstInput[i]);
        if (validate) {
          isValidate = false;
        }
      }
    }
  }
  return {
    isValidate: isValidate,
  };
};
/**
 * Hàm validate ô input
 * Created by NVTAN(04/04/2023)
 */
const validateInput = (input) => {
  var component = input.__vueParentComponent;
  // Lấy ra danh sách prop
  var props = component.props;
  var setup = component.setupState;
  // Lấy ra danh sách validate
  var rules = props.rules;
  var name = props.name;
  var val = input.value;
  let inValid = true;
  if (rules && rules.length) {
    for (var i = 0; i < rules.length; i++) {
      if (rules[i] == "Empty") {
        if (!val || val.trim() == "") {
          setup.isValidate = true;
          setup.messageValidate = name + " không được để trống.";
          inValid = true;
        } else {
          inValid = false;
        }
      } else if (rules[i] == "Email") {
        const regexEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (!val || val.trim() == "") {
          setup.isValidate = true;
          setup.messageValidate = name + " không được để trống.";
          inValid = true;
        } else if (val && !val.match(regexEmail)) {
          setup.isValidate = true;
          setup.messageValidate = name + " không đúng định dạng.";
          inValid = true;
        } else {
          inValid = false;
        }
      } else if (rules[i] == "Password") {
        const regexPassword = new RegExp(
          "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"
        );
        if (!val || val.trim() == "") {
          setup.isValidate = true;
          setup.messageValidate = name + " không được để trống.";
          inValid = true;
        } else if (val && !regexPassword.test(val)) {
          setup.isValidate = true;
          setup.messageValidate =
            " Tối thiểu 8 ký tự và có chứa ít nhất một chữ cái và một chữ số";
          inValid = true;
        } else {
          inValid = false;
        }
      }

      return inValid;
      // else if (rules[i] == "Number") {
      //   const regexNumber = /^\d+$/;
      //   if (val && !val.match(regexNumber)) {
      //     setup.isEmpty = true;
      //     setup.validateError = name + " không đúng định dạng.";
      //     return {
      //       isValid: false,
      //       textValidate: setup.validateError,
      //     };
      //   } else {
      //     return {
      //       isValid: true,
      //     };
      //   }
      // }
    }
  }
};

export { validateData, validateInput };
