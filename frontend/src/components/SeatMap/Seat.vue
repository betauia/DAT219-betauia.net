<template>
  <div class="seat">{{seat.id}}</div>
</template>

<script>
export default {
  props: {
    seat: Object
  },
  created() {}
};
</script>

<style>
.seat {
  background-color: rgb(175, 175, 175);
  float: left;
  width: 30px;
  height: 30px;
  text-align: center;
  border-style: dashed;
  border-width: 1px;
}
</style>
