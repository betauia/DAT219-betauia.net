module.exports = {
    devServer: {
        port: 8080,
        proxy: {
            '/api': {
                target: 'http://beta_backend:5001',
                ws: true,
                secure: false,
                changeOrigin: true,
            },
        },
    },
};