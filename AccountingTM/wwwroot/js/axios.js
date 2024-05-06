axios.interceptors.response.use(
    (response) => {
        return response;
    },
    async (error) => {
        const originalConfig = error.config;

        if (error.config.showError === false) {
            return Promise.resolve();
        }

        const errorResponse = error.response;
        if (!errorResponse) {
        } else {
        //    const message = errorResponse.data?.Error?.Message;

        //    if (errorResponse.status === 400) {
        //        notify.error({ message: message });
        //    } else {
        //        notify.error({ message: 'Что-то пошло не так.' });
        //    }
        }

        console.log(error);

        return Promise.reject(error);
    },
);