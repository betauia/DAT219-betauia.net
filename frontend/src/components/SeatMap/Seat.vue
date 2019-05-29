<template>
  <div class="seat" :style="style">{{seat.id}}</div>
</template>

<script>
export default {
  props: {
    seat: Object
  },
  data: function() {
    return {
      rect: {}
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
    }
  },
  created() {
    var grid = document.getElementById("grid");

    var top = this.getY(grid);
    var left = this.getX(grid);

    this.rect.x = left + this.seat.x;
    this.rect.y = top + this.seat.y;
  },
  computed: {
    style() {
      return "left: " + this.rect.x + "px;" + " top: " + this.rect.y + "px;";
    }
  }
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
