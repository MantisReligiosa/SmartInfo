<template>
    <v-select
            :items="formats"
            item-value="id"
            :model-value="block.formatId"
            @update:modelValue="setFormatId"
            density="compact"
            variant="underlined"
            class="mx-2"
    ></v-select>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import * as Constants from "@/constants"
import {IDatetimeBlock} from "@/interfaces/Blocks";

const dStore = deviceStore()
const block = computed(() => dStore.block as IDatetimeBlock)

const formats = computed(() => [
    {id: Constants.datetimeFormats.ddmmyyyy, title: 'ДД.ММ.ГГГГ'},
    {id: Constants.datetimeFormats.dayOfWeek_ddmmyyyy, title: 'деньНедели, ДД.ММ.ГГГГ'},
    {id: Constants.datetimeFormats.dayOfWeek_ddmmyyyy_hhmm, title: 'деньНедели, ДД.ММ.ГГГГ ЧЧ:ММ'},
    {id: Constants.datetimeFormats.dayOfWeek_ddmmyyyy_hhmmss, title: 'деньНедели, ДД.ММ.ГГГГ ЧЧ:ММ:СС'},
    {id: Constants.datetimeFormats.ddmmyyyy_hhmm, title: 'ДД.ММ.ГГГГ ЧЧ:ММ'},
    {id: Constants.datetimeFormats.ddmmyyyy_hhmmss, title: 'ДД.ММ.ГГГГ ЧЧ:ММ:СС'},
    {id: Constants.datetimeFormats.dd_month, title: 'ДД месяц'},
    {id: Constants.datetimeFormats.month_yyyy, title: 'месяц ГГГГ'},
    {id: Constants.datetimeFormats.hhmm, title: 'ЧЧ:ММ'},
    {id: Constants.datetimeFormats.hhmmss, title: 'ЧЧ:ММ:СС'},
])

const setFormatId = (id: number) => {
    if (!block.value)
        return
    block.value.formatId = id
}

</script>

<style scoped>

</style>