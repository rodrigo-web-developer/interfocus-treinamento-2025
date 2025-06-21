export default function Modal({ children, open }) {
    return <div className={`modal ${open ? "" : "hidden"}`}>
        {children}
    </div>
}
