import Toast from "react-native-toast-message"

const ToastService = {
    Error(title, message) {
        Toast.show({
            type: 'error',
            text1: title,
            text2: message,
        });
    },
    Success(title, message) {
        Toast.show({
            type: 'success',
            text1: title,
            text2: message,
            topOffset: 120,
        });
    }
}

export default ToastService;