module.exports = {
    devServer: {
        port: 80,
        proxy: {
            '^/api': {
                target: 'https://localhost:5001',
                ws: true,
                changeOrigin: true,
            },
        },
    }
}
