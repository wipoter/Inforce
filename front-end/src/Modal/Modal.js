import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Modal.css';

const Modal = ({ isOpen, onClose, onSubmit }) => {
    const [longUrl, setLongUrl] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (longUrl) {
            onSubmit(longUrl)
                .then(() => {
                    setLongUrl('');
                    onClose();
                })
                .catch((error) => {
                    console.error('Error submitting URL:', error);
                });
        }
    };

    return (
        <div className={`modal ${isOpen ? 'show' : ''}`} style={{ display: isOpen ? 'block' : 'none' }}>
            <div className="modal-overlay" onClick={onClose}></div>
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Додати URL</h5>
                        <button type="button" className="close" onClick={onClose}>
                            <span>&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="longUrl">Довгий URL</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="longUrl"
                                    value={longUrl}
                                    onChange={(e) => setLongUrl(e.target.value)}
                                    placeholder="Введіть довгий URL"
                                    required
                                />
                            </div>
                            <button type="submit" className="btn btn-primary">Додати</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Modal;
