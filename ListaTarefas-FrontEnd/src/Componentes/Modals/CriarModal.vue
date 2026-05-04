<script setup>
import { ref } from "vue";
import { notify } from "@kyvg/vue3-notification";
const emit = defineEmits(["fechar", "salvar"]);

const form = ref({
	titulo: "",
	descricao: "",
	status: 0,
});

function salvar() {
	if (!form.value.titulo.trim()) {
		notify({ type: "error", text: "O título é obrigatório." });
		return;
	}
	if (form.value.titulo.trim().length < 3) {
		notify({
			type: "error",
			text: "O título deve ter no mínimo 3 caracteres.",
		});
		return;
	}
	if (!form.value.descricao.trim()) {
		notify({ type: "error", text: "A descrição é obrigatória." });
		return;
	}

	emit("salvar", { ...form.value });
	emit("fechar");
}
</script>

<template>
	<div class="overlay" @click.self="emit('fechar')">
		<div class="modal">
			<h2 class="modal-titulo">Nova Tarefa</h2>

			<label>Título</label>
			<input v-model="form.titulo" placeholder="Ex: Fazer compras" />

			<label>Descrição</label>
			<input v-model="form.descricao" placeholder="Detalhes da tarefa..." />

			<label>Status</label>
			<select v-model="form.status">
				<option :value="0">Pendente</option>
				<option :value="1">Em Andamento</option>
				<option :value="2">Concluída</option>
			</select>

			<div class="modal-acoes">
				<button class="btn-cancelar" @click="emit('fechar')">Cancelar</button>
				<button class="btn-salvar" @click="salvar">Criar</button>
			</div>
		</div>
	</div>
</template>

<style>
.overlay {
	position: fixed;
	inset: 0;
	background: rgba(0, 0, 0, 0.6);
	display: flex;
	align-items: center;
	justify-content: center;
	z-index: 100;
}

.modal {
	background-color: var(--gray-500);
	border: 1px solid var(--gray-400);
	border-radius: 12px;
	padding: 32px;
	width: 100%;
	max-width: 480px;
	display: flex;
	flex-direction: column;
	gap: 8px;
}

.modal-titulo {
	font-size: 1.25rem;
	font-weight: 700;
	color: var(--blue);
	margin-bottom: 8px;
}

.modal label {
	font-size: 0.8rem;
	font-weight: 600;
	color: var(--gray-300);
	margin-top: 8px;
}

.modal input,
.modal textarea,
.modal select {
	font-size: 0.875rem;
	color: var(--gray-100);
	background-color: var(--gray-600);
	border: 1px solid var(--gray-400);
	border-radius: 6px;
	padding: 10px 14px;
	outline: none;
	transition: border-color 0.15s ease;
}

.modal input:focus,
.modal textarea:focus,
.modal select:focus {
	border-color: var(--purple);
}

.modal textarea {
	resize: vertical;
	min-height: 90px;
}

.modal-acoes {
	display: flex;
	justify-content: flex-end;
	gap: 10px;
	margin-top: 16px;
}

.btn-cancelar {
	font-size: 0.875rem;
	font-weight: 600;
	padding: 10px 20px;
	border-radius: 6px;
	border: 1px solid var(--gray-400);
	background: transparent;
	color: var(--gray-300);
	cursor: pointer;
	transition: border-color 0.15s ease;
}

.btn-cancelar:hover {
	border-color: var(--gray-100);
	color: var(--gray-100);
}

.btn-salvar {
	font-size: 0.875rem;
	font-weight: 700;
	padding: 10px 20px;
	border-radius: 6px;
	border: none;
	background-color: var(--blue-dark);
	color: var(--gray-100);
	cursor: pointer;
	transition: filter 0.15s ease;
}

.btn-salvar:hover {
	filter: brightness(1.2);
}
</style>
