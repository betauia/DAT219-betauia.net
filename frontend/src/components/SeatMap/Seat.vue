<template>
  <VueDragResize
    class="seat"
    :isActive="true"
    :snapToGrid="true"
    :gridX="20"
    :gridY="20"
    :w="80"
    :h="80"
    :x="400"
    :y="200"
    style="background-color: yellow;"
  >{{seat.id}}</VueDragResize>
</template>

<script>
import VueDragResize from "@/components/SeatMap/vue-drag-resize.vue";

export default {
  components: {
    VueDragResize
  },
  props: {
    seat: Object
  },
  created() {
    document.getElementById(this.seat.id).style.top = "50px";
    document.getElementById(this.seat.id).style.left = "50px";
  }
};
</script>

<style>
.seat {
  position: absolute;
  background-color: yellow;
  width: 50px;
  height: 50px;
  text-align: center;
}
</style>
