module.exports = {
    devServer: {
        port: 8080,
        proxy: {
            '^/api': {
                target: 'https://localhost:5001',
                ws: true,
                changeOrigin: true,
            },
        },
    }
}
