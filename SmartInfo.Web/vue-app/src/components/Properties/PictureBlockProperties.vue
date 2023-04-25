<template>
    <v-container>
        <v-row>
            <v-file-input
                    type="file"
                    accept="image/*"
                    ref="file"
                    style="display: none"
                    @update:model-value="fileChosen"
            ></v-file-input>
            <v-btn
                    @click="openDialog"
                    prepend-icon="mdi-file-upload-outline"
            >Выбрать
            </v-btn>
        </v-row>
        <v-row>
            <v-radio-group
                    label="Размещение"
                    v-model="block.imageMode"
            >
                <v-radio
                        label="Обрезать"
                        :value="Constants.imageMode.crop"
                ></v-radio>
                <v-radio
                        label="Вписать"
                        :value="Constants.imageMode.zoom"
                ></v-radio>
            </v-radio-group>
        </v-row>
        <v-row>
            <v-switch
                    label="Сохраняя пропорции"
                    :disabled="block.imageMode==Constants.imageMode.crop"
                    v-model="block.saveProportions"
            ></v-switch>
        </v-row>
    </v-container>
</template>

<script setup lang="ts">
import * as Constants from "@/constants"
import {computed, ref} from 'vue'
import {deviceStore} from "@/store/deviceStore";
import {IPictureBlock} from "@/interfaces/Blocks";

const file = ref<HTMLInputElement | null>(null)

const dStore = deviceStore()
const block = computed(() => dStore.block as IPictureBlock)

const openDialog = () => {
    if (file.value) {
        file.value.click()
    }
}

const fileChosen = (files: File[]) => {
    const reader = new FileReader();
    reader.addEventListener('load', fileLoaded);
    reader.addEventListener('error', alert);
    reader.readAsDataURL(files[0]);
}

const fileLoaded = (e: ProgressEvent<FileReader>) => {
    if (!e.target)
        return
    let binaryString = e.target.result as string
    block.value.base64 = btoa(binaryString)
}

</script>