<template>
    <div>
        <head>
            <!-- Add meta tags for mobile and IE -->
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        </head>

        <body class="is-mobile center">
            
            <div>
              <img src="@/assets/img/Betal med Vipps - 140px.png" v-on:click=""/>
            </div>
            
            <!-- Set up a container element for the button -->
            <div id="paypal-button-container"></div>

            
        </body>
    
    </div>
</template>

<script src="https://www.paypal.com/sdk/js?client-id=sb&currency=USD"></script>
 
<script>  
export default {
    mounted() {
        // Render the PayPal button into #paypal-button-container
        paypal.Buttons({
            style: {
                layout: 'horizontal'
            }
        }).render('#paypal-button-container');
    }
}
</script>

<style>
/* Media query for mobile viewport */
        @media screen and (max-width: 400px) {
            #paypal-button-container {
                width: 100%;
            }
        }
        
        /* Media query for desktop viewport */
        @media screen and (min-width: 400px) {
            #paypal-button-container {
                width: 250px;
            }
        }
</style>
