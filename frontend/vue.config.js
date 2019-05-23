module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://192.168.1.101:5001',
        ws: true,
        changeOrigin: true,
      },
    },
  },
};
