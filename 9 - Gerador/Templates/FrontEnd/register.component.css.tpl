.modal-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border-radius: 0.375rem 0.375rem 0 0;
}

.modal-title {
  font-weight: 600;
  margin: 0;
}

.btn-close {
  background: none;
  border: none;
  color: white;
  font-size: 1.25rem;
  opacity: 0.8;
  transition: opacity 0.2s;
}

.btn-close:hover {
  opacity: 1;
}

.modal-body {
  padding: 1.5rem;
}

.modal-footer {
  padding: 1rem 1.5rem;
  background-color: #f8f9fa;
  border-radius: 0 0 0.375rem 0.375rem;
}

.form-label {
  font-weight: 500;
  color: #495057;
  margin-bottom: 0.5rem;
}

.form-control, .form-select {
  border: 1px solid #ced4da;
  border-radius: 0.375rem;
  padding: 0.75rem;
  font-size: 0.875rem;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

.form-control:focus, .form-select:focus {
  border-color: #667eea;
  box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
}

.btn-outline-secondary {
  border-color: #6c757d;
  color: #6c757d;
}

.btn-outline-secondary:hover {
  background-color: #6c757d;
  border-color: #6c757d;
  color: white;
}

.btn-primary {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  border: none;
  padding: 0.75rem 1.5rem;
  font-weight: 500;
}

.btn-primary:hover {
  background: linear-gradient(135deg, #5a67d8 0%, #6b46c1 100%);
  transform: translateY(-1px);
  box-shadow: 0 4px 8px rgba(102, 126, 234, 0.3);
}

.btn-primary:disabled {
  background: #6c757d;
  transform: none;
  box-shadow: none;
}