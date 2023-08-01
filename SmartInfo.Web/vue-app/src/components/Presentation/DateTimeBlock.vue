<script setup lang="ts">
import {defineProps, onMounted, ref, computed} from "vue";
import PresentationBlock from "@/components/Presentation/PresentationBlock.vue";
import {IDatetimeBlock} from "@/interfaces/Blocks";
import moment from "moment";
import {mainStore} from "@/store/mainStore";

const mStore = mainStore()

const props = defineProps(['block', 'scale'])
const block = computed(() => props.block as IDatetimeBlock)
const datetimeFormat = computed(() => mStore.dateTimeFormats.find(f => f.Value === block.value.formatId)?.MomentFormat ?? '');
const dateTime = ref('')

onMounted(() => {
  setInterval(updateTime, 500)
})

const updateTime = () => {
  dateTime.value = moment().format(datetimeFormat.value)
}
</script>

<template>
  <presentation-block
      v-bind:block="props.block"
      v-bind:scale="props.scale"
  >{{ dateTime }}
  </presentation-block>
</template>

<style scoped>
</style>