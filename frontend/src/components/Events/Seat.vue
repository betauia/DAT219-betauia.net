<template>
  <div class="seat" :style="style" @click="clickSeat"> {{seat.number}}</div>
</template>

<script>
export default {
  props: {
    seat: Object
  },
  data: function() {
    return {
      rect: {},
      backColor: String,
      reserved: Boolean = false,
    };
  },
  methods: {
    getY(oElement) {
      var iReturnValue = 0;
      while (oElement != null) {
        iReturnValue += oElement.offsetTop;
        oElement = oElement.offsetParent;
      }
      return iReturnValue;
    },
    getX(oElement) {
      var iReturnValue = 0;
      while (oElement != null) {
        iReturnValue += oElement.offsetLeft;
        oElement = oElement.offsetParent;
      }
      return iReturnValue;
    },
    clickSeat: function(event){
      if(this.seat.isAvailable){
        this.reserved = !this.reserved;
        this.setColor();
        this.$emit('clicked',this.seat.id,this.reserved);
      }
    },
    setColor: function(){
      console.log(this.reserved);
      if(this.reserved){
        this.backColor = "blue";
      }else if(this.seat.isAvailable) {
        this.backColor = "green";
      }else {
        this.backColor = "grey";
      }
    },
  },
  created() {
    var grid = document.getElementById('grid');

    var top = this.getY(grid);
    var left = this.getX(grid);


    this.rect.x = (this.seat.x);
    this.rect.y = (this.seat.y);
    this.reserved = this.seat.isReserved;
    this.setColor()
  },
  computed: {
    style() {
      return "left: " + this.rect.x + "px;" + " top: " + this.rect.y + "px;" + "backgroundColor:"+this.backColor + ";" + "cursor:pointer;";
    }
  },
};
</script>

<style>
.seat {
  background-color: rgb(175, 175, 175);
  width: 30px;
  height: 30px;
  text-align: center;
  position: absolute;
  border-style: dashed;
  border-width: 1px;
}
</style>
