@import 'shared';

:host {
  --modal-background: #{palette-color('white')};
  --modal-border-radius: 6px;
  --modal-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  --header-background: #{palette-color('grey-1')};
  --header-border: #{palette-color('grey-2')};
}

// Modal Container
.register-modal-container {
  background: var(--modal-background);
  border-radius: var(--modal-border-radius);
  box-shadow: var(--modal-shadow);
  overflow: hidden;
  max-width: 500px;
  width: 100%;
  max-height: 90vh;
  display: flex;
  flex-direction: column;
}

// Modal Header
.modal-header-custom {
  background: var(--header-background);
  padding: 20px 24px;
  border-bottom: 1px solid var(--header-border);
  position: relative;
  display: flex;
  align-items: center;
  justify-content: space-between;

  .modal-title {
    margin: 0;
    font-size: 20px;
    font-weight: 600;
    color: #{palette-color('grey-9')};
  }

  .btn-close-custom {
    background: none;
    border: none;
    color: #{palette-color('grey-6')};
    cursor: pointer;
    padding: 4px;
    border-radius: 4px;
    transition: all 0.2s ease;

    &:hover {
      background: #{palette-color('grey-2')};
      color: #{palette-color('grey-8')};
    }

    i {
      font-size: 14px;
    }
  }
}

// Modal Body
.modal-body-custom {
  padding: 24px;
  flex: 1;
  overflow-y: auto;

  .register-form {
    .form-section {
      .form-group {
        margin-bottom: calc(var(--default-spacing) * 3);

        .col-form-label {
          font-weight: 500;
          color: #{palette-color('grey-8')};
        }

        .form-control {
          background-color: #{palette-color('grey-0')};
          border: 1px solid #{palette-color('grey-3')};
          border-radius: 4px;
          padding: 8px 12px;
          font-size: 14px;

          &:focus {
            border-color: #{palette-color('primary')};
            box-shadow: 0 0 0 2px #{palette-color('primary', 0.1)};
          }
        }

        .form-text {
          font-size: 12px;
          margin-top: 4px;
        }
      }
    }
  }
}

// Form Actions
.form-actions {
  display: flex;
  gap: 12px;
  justify-content: flex-end;
  padding: 20px 24px;
  border-top: 1px solid var(--header-border);
  background: var(--header-background);

  .btn {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 8px 16px;
    border-radius: 4px;
    font-weight: 500;
    font-size: 14px;
    transition: all 0.2s ease;
    cursor: pointer;
    border: 1px solid transparent;
    min-width: 100px;
    justify-content: center;

    i {
      font-size: 12px;
    }

    &.btn-outline-secondary {
      background: transparent;
      color: #{palette-color('grey-7')};
      border-color: #{palette-color('grey-3')};

      &:hover {
        background: #{palette-color('grey-2')};
        color: #{palette-color('grey-8')};
      }
    }

    &.btn-primary {
      background: #{palette-color('primary')};
      color: white;
      border-color: #{palette-color('primary')};

      &:hover:not(:disabled) {
        background: #{palette-color('primary', 0.8)};
        border-color: #{palette-color('primary', 0.8)};
      }

      &:disabled {
        background: #{palette-color('grey-4')};
        border-color: #{palette-color('grey-4')};
        cursor: not-allowed;
        opacity: 0.6;
      }
    }
  }
}

// Responsive Design
@media (max-width: 768px) {
  .register-modal-container {
    margin: 16px;
    max-height: calc(100vh - 32px);
  }

  .modal-header-custom {
    padding: 16px 20px;

    .modal-title {
      font-size: 18px;
    }
  }

  .modal-body-custom {
    padding: 20px;
  }

  .form-actions {
    padding: 16px 20px;
    flex-direction: column-reverse;

    .btn {
      width: 100%;
      min-width: unset;
    }
  }
}