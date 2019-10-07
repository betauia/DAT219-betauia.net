module.exports = {
    devServer: {
        port: 8080,
        proxy: {
            '/api': {
                target: 'http://localhost:5001',
                ws: true,
                secure: false,
                changeOrigin: true,
            },
        },
    },
};
