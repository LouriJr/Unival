import React from 'react'
import styles from './CardUsuario.module.css'

export default function CardUsuario({ usuario }) {
    return (
        <div>
            <span>E-mail: {usuario.email}</span>
            <img className={styles.imagem} src={usuario.imagemURL} alt={usuario.name} />
        </div>
    )
}
