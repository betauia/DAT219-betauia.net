module.exports = {
    devServer: {
        port: 80,
        proxy: {
            '^/api': {
                target: 'https://betauia-backend.azurewebsites.net:443',
                ws: true,
                changeOrigin: true,
            },
        },
    }
}
